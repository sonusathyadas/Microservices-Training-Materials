using EventApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EventApi.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);
            this.SupportedMediaTypes.Add("text/csv");
            this.SupportedMediaTypes.Add("application/csv");
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(EventData).IsAssignableFrom(type) || typeof(IEnumerable<EventData>).IsAssignableFrom(type))
                return true;
            else
                return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var buffer = new StringBuilder();
            var response = context.HttpContext.Response;
            if (context.Object is EventData)
            {
                var item = context.Object as EventData;
                buffer.Append("Id,Title,Description, StartDate, EndDate, Location, Organizer, RegistrationUrl, LastDate" +  Environment.NewLine);
                buffer.Append($"{item.Id},{item.Title},{item.Description},{item.StartDate},{item.EndDate},{item.Location},{item.Organizer},{item.RegistrationUrl},{item.LastDate}");
            }
            else if (context.Object is IEnumerable<EventData>)
            {
                var items = context.Object as IEnumerable<EventData>;
                buffer.Append("Id,Title,Description, StartDate, EndDate, Location, Organizer, RegistrationUrl, LastDate" + Environment.NewLine);
                foreach (var item in items)
                {
                    buffer.Append($"{item.Id},{item.Title},{item.Description},{item.StartDate},{item.EndDate},{item.Location},{item.Organizer},{item.RegistrationUrl},{item.LastDate}" + Environment.NewLine);
                }
            }
            await response.WriteAsync(buffer.ToString(), selectedEncoding); //using Microsoft.AspNetCore.Http
        }
    }
}
