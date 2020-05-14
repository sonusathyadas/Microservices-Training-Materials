using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApi.Formatters;
using EventApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EventApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.OutputFormatters.Add(new CsvOutputFormatter());
            })
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title= "Events API",
                    Version ="1.0",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name="Sonu Sathyadas",
                        Email="sonusathyadas@hotmail.com"
                    },
                    Description="APIs for event management system"                  
                });
            });

            services.AddCors(c=>
            {
                c.AddDefaultPolicy(options =>
                {
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });

                c.AddPolicy("Partners", options =>
                {

                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                    //options
                    //.WithOrigins("http://www.synergetics.com")
                    //.AllowAnyMethod()
                    //.AllowAnyHeader();

                    //options
                    //.WithOrigins("https://microsoft.com")
                    //.WithMethods("GET")
                    //.AllowAnyHeader();

                    //options
                    //   .WithOrigins("http://127.0.0.1:5500")
                    //   .AllowAnyMethod()
                    //   .AllowAnyHeader();
                });
            });

            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("EventsConnection"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {                    
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("JwtSettings")["Issuer"],
                        ValidAudience = Configuration.GetSection("JwtSettings")["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSettings")["Secret"]))
                    };
                });                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "EventAPI");
                    options.RoutePrefix = "";
                });
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
