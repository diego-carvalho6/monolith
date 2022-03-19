using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IItemServices
    {
        Task<IEnumerable<Entities.Item>> GetAll();
        Task<object> Insert(Entities.Item item);
        Task<Entities.Item> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.Item> Put(Entities.Item item);
    }
}