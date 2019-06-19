using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventGridDemo.Models;
using EventGridDemo.ServiceHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventGridDemo.Controllers
{
    [Route("api/[controller]")]    
    public class UpdatesController : Controller
    {
        #region Data members
        private IConfiguration config;
        private bool EventTypeSubcriptionValidation
            => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() == "SubscriptionValidation";
        private bool EventTypeNotification
            => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() =="Notification";
        #endregion

        #region Constructor
        public UpdatesController(IConfiguration configuration)
        {
            this.config = configuration;
            CosmosHelper.EndpointUri = config.GetValue<string>("CosmosDB:EndpointUri");
            CosmosHelper.Key = config.GetValue<string>("CosmosDB:Key");
        }
        #endregion

        #region API methods
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var jsonContent = await reader.ReadToEndAsync();
                Console.WriteLine(jsonContent);
                if (EventTypeSubcriptionValidation)
                {
                    return await HandleValidation(jsonContent);
                }
                else if (EventTypeNotification)
                {
                    if (IsCloudEvent(jsonContent))
                    {
                        return await HandleCloudEvent(jsonContent);
                    }
                    return await HandleGridEvents(jsonContent);
                }
                return BadRequest();
            }
        }

        #endregion

        #region Private methods
        private async Task<JsonResult> HandleValidation(string jsonContent)
        {
            var gridEvent =JsonConvert.DeserializeObject<List<GridEvent<Dictionary<string, string>>>>(jsonContent)
                    .First();
            await CosmosHelper.CreateDocumentAsync("eventgriddata", "events", gridEvent);
            // Retrieve the validation code and echo back.
            var validationCode = gridEvent.Data["validationCode"];
            return new JsonResult(new
            {
                validationResponse = validationCode
            });
        }

        private async Task<IActionResult> HandleGridEvents(string jsonContent)
        {
            var events = JArray.Parse(jsonContent);
            foreach (var e in events)
            {                
                var details = JsonConvert.DeserializeObject<GridEvent<dynamic>>(e.ToString());
                await CosmosHelper.CreateDocumentAsync("eventgriddata", "events", details);
            }
            return Ok();
        }

        private async Task<IActionResult> HandleCloudEvent(string jsonContent)
        {
            var details = JsonConvert.DeserializeObject<CloudEvent<dynamic>>(jsonContent);
            await CosmosHelper.CreateDocumentAsync("eventgriddata", "events", details);
            return Ok();
        }

        private static bool IsCloudEvent(string jsonContent)
        {
            // Cloud events are sent one at a time, while Grid events
            // are sent in an array. As a result, the JObject.Parse will 
            // fail for Grid events. 
            try
            {
                // Attempt to read one JSON object. 
                var eventData = JObject.Parse(jsonContent);

                // Check for the cloud events version property.
                var version = eventData["cloudEventsVersion"].Value<string>();
                if (!string.IsNullOrEmpty(version)) return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        #endregion
    }
}