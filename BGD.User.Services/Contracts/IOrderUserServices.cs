using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IOrderUserServices
    {
        Task<IEnumerable<Entities.OrderUser>> GetAll();
        Task<object> Insert(Entities.OrderUser orderUser);
        Task<Entities.OrderUser> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.OrderUser> Put(Entities.OrderUser orderUser);
    }
}