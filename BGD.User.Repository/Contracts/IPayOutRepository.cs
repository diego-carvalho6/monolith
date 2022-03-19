using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IPayOutRepository
    {
        Task<IEnumerable<Entities.PayOut>> GetAsync();
        Task<object> InsertAsync(Entities.PayOut order);
        Task<IEnumerable<Entities.PayOut>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.PayOut> UpdateAsync(Entities.PayOut payOut);
    }
}