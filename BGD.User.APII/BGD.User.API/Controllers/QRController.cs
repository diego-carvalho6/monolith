using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace BGD.User.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class QRController : ControllerBase
    {
        private readonly IQRServices _service;
        public QRController(IQRServices service)
        {
            _service = service;
        }
        [HttpGet("{tenant}/showAll")]
        public async Task<IEnumerable<QR>> FindAll(string tenant)
        {
            var result = await _service.GetAll();
            return result;
        }
        // [Authorize(Roles = ("admin,staff"))]
        [HttpGet("showAll")]
        public async Task<IEnumerable<QR>> GetAll()
        { 
            var result = await _service.GetAll();
            return result;
        }
        [Authorize]
        [HttpGet("generate/{tenant}/{id}")]
        public async Task<IActionResult> Get(string tenant, string id, [FromQuery]string time)
        {
            var result = await _service.Get(id);
            using var stream = new MemoryStream();
            result.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return File(stream.ToArray(), "image/jpeg");
        }
        [Authorize]
        [HttpPost("generate/{tenant}")]
        public async Task<ActionResult<QR>> PostUnique(string tenant, [FromBody] QR code)
        {
            
            var result = await _service.Insert(code,null, tenant);
            return Redirect("http://www.google.com");
        }
        [HttpPost("generate")]
        public async Task<ActionResult<QR>> Post([FromBody] QR code)
        {
            var user = User;
            var result = await _service.Insert(code, user);
            return Ok();
        }
        [HttpPut("")]
        public async Task<ActionResult<QR>> Put([FromBody] QR code)
        {
            var result = await _service.Put(code);
            return new ActionResult<QR>(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        { 
            var result = await _service.Delete(id);
            return NoContent();
        }
    }
}