using System;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyValueController : ControllerBase
    {
        private readonly IBuyValueServices _service;
        public BuyValueController(IBuyValueServices service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Entities.BuyValue>> Get()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.BuyValue>> Find(Guid id)
        {
            var users = await _service.Get(id);
            return Ok(users);
        }
        
        [HttpPost]
        public async Task<ActionResult<Entities.User>> Post([FromBody]Entities.BuyValue buyValue)
        {
            var result = await _service.Insert(buyValue);
            return Ok(result);
        }
        [HttpPut("")]
        public async Task<ActionResult<Entities.User>> Put([FromBody]Entities.BuyValue buyValue)
        {
            var result = await _service.Put(buyValue);
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