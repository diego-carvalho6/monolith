using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IBuyValueRepository
    {
        Task<IEnumerable<Entities.BuyValue>> GetAsync();
        Task<object> InsertAsync(Entities.BuyValue buyValue);
        Task<IEnumerable<Entities.BuyValue>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.BuyValue> UpdateAsync(Entities.BuyValue buyValue);   
    }
}