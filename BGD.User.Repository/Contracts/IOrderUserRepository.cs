using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IOrderUserRepository
    {
        Task<IEnumerable<Entities.OrderUser>> GetAsync();
        Task<object> InsertAsync(Entities.OrderUser orderUser);
        Task<IEnumerable<Entities.OrderUser>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.OrderUser> UpdateAsync(Entities.OrderUser orderUser);
    }
}