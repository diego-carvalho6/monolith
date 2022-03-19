using System;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _service;
        public OrderController(IOrderServices service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Entities.Order>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("verifyOrder")]
        public async Task<ActionResult<Entities.Order>> VerifyOrder([FromQuery] VerifyOrderQuery verify)
        {
            var result = await _service.VerifyOrder(verify);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.Order>> Find(Guid id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Entities.Order>> Post([FromBody]Entities.Order order)
        {
            var result = await _service.Insert(order);
            return Ok(result);
        }
        [HttpPut("")]
        public async Task<ActionResult<Entities.Order>> Put([FromBody]Entities.Order order)
        {
            var result = await _service.Put(order);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}