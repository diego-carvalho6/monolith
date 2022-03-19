using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<Entities.OrderItem>> GetAsync();
        Task<object> InsertAsync(Entities.OrderItem orderItem);
        Task<IEnumerable<Entities.OrderItem>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.OrderItem> UpdateAsync(Entities.OrderItem orderItem);
    }
}