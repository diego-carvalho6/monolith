using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IItemRepository
    {
        Task<IEnumerable<Entities.Item>> GetAsync();
        Task<object> InsertAsync(Entities.Item item);
        Task<IEnumerable<Entities.Item>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.Item> UpdateAsync(Entities.Item item);
        Task<dynamic> GetOrders(Guid id);
    }
}