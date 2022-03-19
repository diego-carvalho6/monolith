using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IOrderItemServices
    {
        Task<IEnumerable<Entities.OrderItem>> GetAll();
        Task<object> Insert(Entities.OrderItem orderItem);
        Task<bool> InsertMany(IEnumerable<Entities.OrderItem> orderItem);
        Task<Entities.OrderItem> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.OrderItem> Put(Entities.OrderItem orderItem);
    }
}