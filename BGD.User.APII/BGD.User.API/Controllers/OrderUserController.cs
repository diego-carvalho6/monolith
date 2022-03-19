using System;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderUserController : ControllerBase
    {
        private readonly IOrderUserServices _service;
        public OrderUserController(IOrderUserServices service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Entities.OrderUser>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.OrderUser>> Find(Guid id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }
    
        [HttpPost]
        public async Task<ActionResult<Entities.OrderUser>> Post([FromBody]Entities.OrderUser orderUser)
        {
            var result = await _service.Insert(orderUser);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<Entities.OrderUser>> Put([FromBody]Entities.OrderUser orderUser)
        {
            var result = await _service.Put(orderUser);
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
