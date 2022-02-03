using System;
using System.Threading.Tasks;
using MassTransit;
using Microservice_service1.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_service1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider bus;

        public OrderController(ISendEndpointProvider bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            Uri uri = new Uri("exchange:order-queue");

            var endpoint = await bus.GetSendEndpoint(uri);
            await endpoint.Send(order);

            return Ok("success");
        }
    }
}