using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Entities;

namespace BGD.User.Services.Contracts
{
    public interface IOrderServices
    {
        Task<IEnumerable<Entities.Order>> GetAll();
        
        Task<Entities.Order> VerifyOrder(VerifyOrderQuery verify);
        
        Task<object> Insert(Entities.Order order);

        Task<Entities.Order> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.Order> Put(Entities.Order order);
        
    }
}