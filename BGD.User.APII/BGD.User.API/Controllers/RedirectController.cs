using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanoid;

namespace BGD.User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedirectController : ControllerBase
    {
        private readonly IRedirectServices _service;
        public RedirectController(IRedirectServices service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("{tenant}/{id}")]
        public async Task<IEnumerable<string>> Get(string tenant, string id)
        {
            var result = await _service.GetUrl(id, tenant);
            return result;
        }
        
        
    }
}