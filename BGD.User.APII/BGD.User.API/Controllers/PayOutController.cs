using System;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayOutController : ControllerBase
    {
        private readonly IPayOutServices _service;
        public PayOutController(IPayOutServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Entities.PayOut>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.PayOut>> Find(Guid id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Entities.PayOut>> Post([FromBody]Entities.PayOut payOut)
        {
            var result = await _service.Insert(payOut);
            return Ok(result);
        }
        [HttpPut("")]
        public async Task<ActionResult<Entities.PayOut>> Put([FromBody]Entities.PayOut payOut)
        {
            var result = await _service.Put(payOut);
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