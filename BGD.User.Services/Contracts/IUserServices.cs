using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IUserServices
    {
        Task<IEnumerable<Entities.User>> GetAll();
        Task<Entities.User> Insert(Entities.User user, Entities.Tenant tenant = null);
        Task<object> Login(Entities.User user, Entities.Tenant tenant = null);
        Task<Entities.User> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.User> Put(Entities.User user);
        Task<Entities.User> UpdateUserStatus(Entities.User user);
    }
}
