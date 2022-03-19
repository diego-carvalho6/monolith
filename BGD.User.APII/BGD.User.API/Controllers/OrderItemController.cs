using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemServices _service;
        public OrderItemController(IOrderItemServices service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Entities.OrderItem>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.OrderItem>> Find(Guid id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }

        [HttpPost("/bulkinsert")]
        public async Task<ActionResult<IEnumerable<Entities.OrderItem>>> BulkInsert([FromBody]IEnumerable<Entities.OrderItem> orderItem)
        {
            var result = await _service.InsertMany(orderItem);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Entities.OrderItem>> Post([FromBody]Entities.OrderItem orderItem)
        {
            var result = await _service.Insert(orderItem);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<Entities.OrderItem>> Put([FromBody]Entities.OrderItem orderItem)
        {
            var result = await _service.Put(orderItem);
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