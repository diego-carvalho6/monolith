using System;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _service;
        public ItemController(IItemServices service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Entities.Item>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.Item>> Find(Guid id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Entities.Item>> Post([FromBody]Entities.Item material)
        {
            var result = await _service.Insert(material);
            return Ok(result);
        }
        [HttpPut("")]
        public async Task<ActionResult<Entities.Item>> Put([FromBody]Entities.Item material)
        {
            var result = await _service.Put(material);
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