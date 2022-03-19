using System;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BGD.User.Repository.Dapper.Postgres.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace BGD.User.API
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;
        public UserController(IUserServices service, IPostgresConnectionFactory teste)
        {
            _service = service;
        }
        
        [HttpGet]
        // [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<Entities.User>> Get()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<Entities.User>> Find(Guid id)
        {
            var user = await _service.Get(id);
            return Ok(user);
        }
        
        [HttpPost]
        // [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<Entities.User>> Post([FromBody]Entities.User user)
        {
            var result = await _service.Insert(user);
            return Created("", result);
        }
        [HttpPost("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Entities.User>> PostNewDatabaseAdmin([FromBody]Entities.User user,[FromQuery]Entities.Tenant tenant)
        {
            var result = await _service.Insert(user, tenant);
            return Created("", result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody]Entities.User user, [FromQuery]Entities.Tenant tenant)
        {
            var result = await _service.Login(user, tenant);
            return Ok(result);
        }
        [HttpPut("")]
        public async Task<ActionResult<Entities.User>> Put([FromBody]Entities.User user)
        {
            var result = await _service.Put(user);
            return Ok(result);
        }
        [HttpPut("status")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<Entities.User>> UpdateStatus([FromBody]Entities.User user)
        {
            var result = await _service.UpdateUserStatus(user);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<int>> Delete(Guid id)
        { 
            await _service.Delete(id);
            return NoContent();
        }
    }


}
