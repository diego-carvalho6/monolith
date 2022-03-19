using System;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListServices _service;
        public ToDoListController(IToDoListServices service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Entities.ToDoList>> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.ToDoList>> Find(Guid id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Entities.ToDoList>> Post([FromBody]Entities.ToDoList toDoList)
        {
            var result = await _service.Insert(toDoList);
            return Ok(result);
        }
        [HttpPut("")]
        public async Task<ActionResult<Entities.ToDoList>> Put([FromBody]Entities.ToDoList toDoList)
        {
            var result = await _service.Put(toDoList);
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