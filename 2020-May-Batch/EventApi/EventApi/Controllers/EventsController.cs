using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApi.Infrastructure;
using EventApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private EventDbContext db;

        public EventsController(EventDbContext dbcontext)
        {
            db = dbcontext;
        }

        //GET /api/events
        //[EnableCors("Partners")]
        [HttpGet("", Name ="GetAllEvents")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EventData>>> GetEventsAsync()
        {
            var events = await db.Events.ToListAsync();
            return Ok(events);
        }

        //GET /api/events/5
        [HttpGet("{id:int:min(1)}", Name ="GetEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<EventData>> GetEventByIdAsync(int id)
        {
            var item = await db.Events.FindAsync(id);
            if(item==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }

        //POST /api/events
        [HttpPost("", Name ="AddEvent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventData>> AddEventAsync(EventData item)
        {
            TryValidateModel(item);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await db.Events.AddAsync(item);
                await db.SaveChangesAsync();
                return CreatedAtRoute("GetEventById",new { id=item.Id }, item);
            }
        }


        //PUT /api/events/5
        [HttpPut("{id:int:min(1)}", Name ="UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventData>> UpdateEventAsync(int id, EventData data)
        {
            var item = await db.Events.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            TryValidateModel(data);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                item.Title = data.Title;
                item.Description = data.Description;
                item.StartDate = data.StartDate;
                item.EndDate = data.EndDate;
                item.RegistrationUrl = data.RegistrationUrl;
                item.Location = data.Location;
                item.LastDate = data.LastDate;
                item.Organizer = data.Organizer;
                //db.Entry(data).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        //DELETE /api/events/5
        [HttpDelete("{id:int:min(1)}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var item = await db.Events.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                db.Events.Remove(item);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}