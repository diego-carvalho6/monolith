using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Entities;

namespace BGD.User.Services.Contracts
{
    public interface IRedirectServices
    {
        Task<IEnumerable<string>> GetUrl(string id, string tenant);

    }
}