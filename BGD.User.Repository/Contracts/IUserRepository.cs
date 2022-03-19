using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BGD.User.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<Entities.User>> GetAsync();
        Task<object> InsertAsync(Entities.User user);
        Task<object> InsertAdminAsync(Entities.User user, Entities.Tenant tenant);
        Task<IEnumerable<Entities.User>> FindAsync(Guid id);
        Task<IEnumerable<dynamic>> QueryAsync(Entities.User user, Entities.Tenant tenant = null);
        Task<IEnumerable<dynamic>> QueryOrdersAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.User> UpdateAsync(Entities.User user);
    }
}
