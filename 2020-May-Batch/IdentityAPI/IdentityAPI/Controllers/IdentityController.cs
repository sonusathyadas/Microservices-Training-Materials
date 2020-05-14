using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAPI.Infrastructure;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IdentityDbContext db;
        private IConfiguration configuration;

        public IdentityController(IdentityDbContext dbContext, IConfiguration config)
        {
            db = dbContext;
            configuration = config;
        }

        //POST /api/identity/register
        [HttpPost("register", Name ="RegisterUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<dynamic>> RegisterAsync(UserInfo user)
        {
            TryValidateModel(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                var output = new
                {
                    user.Id, user.FirstName, user.LastName, user.Email
                };
                return Created("", output);
            }
        }


        //POST /api/identity/token
        [HttpPost("token", Name ="GetToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<dynamic>> GetTokenAsync(LoginModel loginInfo)
        {
            TryValidateModel(loginInfo);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await db.Users.SingleOrDefaultAsync(u => u.Email == loginInfo.Email && u.Password == loginInfo.Password);
            if(user==null)
            {
                return Unauthorized(new { Message = "Invalid username/password" });
            }
            //if user exists send the jwt token
            var token = GenerateToken(user);
            var result = new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                Token = token
            };
            return Ok(result);
        }

        private string GenerateToken(UserInfo user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "eventapi"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "paymentapi"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("JwtSettings:Issuer"),
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}