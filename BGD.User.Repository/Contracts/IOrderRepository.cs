using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Entities;

namespace BGD.User.Repository.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Entities.Order>> GetAsync();
        Task<object> InsertAsync(Entities.Order order);
        Task<IEnumerable<Entities.Order>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.Order> UpdateAsync(Entities.Order order);
        Task<dynamic> GetUsers(Guid id);
        Task<dynamic> GetToDoList(Guid id);
        Task<dynamic> GetPayOuts(Guid id);
        Task<dynamic> GetItems(Guid id);
        Task<dynamic> VerifyOrder(VerifyOrderQuery verify);

    }
}