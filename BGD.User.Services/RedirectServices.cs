using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Entities.Enums;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Helpers;
using Microsoft.AspNetCore.Http;

namespace BGD.User.Services
{
    public class RedirectServices : IRedirectServices
    {
        private readonly IRedirectRepository _repository;
        private readonly JWTServices _jwtServices;
        
        public RedirectServices(IRedirectRepository repository, JWTServices jwtServices)
        {
            _repository = repository;
            _jwtServices = jwtServices;
            
        }

        public async Task<IEnumerable<string>> GetUrl(string id, string tenant)
        {
            var result = await _repository.FindAsync(id, tenant);
            if (result.Count() == 0)
            {
                throw new Exception("INVALID_ID");
            }
            
            var qr = result.FirstOrDefault();
            var token = _jwtServices.GenerateTokenJWTAnonymous(qr.Tenant);
            var resultList = new List<string>();
            var url = $"/cardapio/{qr.Table}";
            resultList.Add(token);
            resultList.Add(url);
            
            return resultList;
        }
    }
}