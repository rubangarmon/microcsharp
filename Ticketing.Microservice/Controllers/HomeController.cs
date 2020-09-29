using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microservices.Ticketing.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ticketing.Microservice.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        public TicketController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.BookedOn = DateTime.Now;
                Uri uri = new Uri("---");
                var endopoint = await _bus.GetSendEndpoint(uri);
                await endopoint.Send(ticket);
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet]
        public async Task<string> Meme()
        {
            return "it's me Mario";
        }
    }
}
