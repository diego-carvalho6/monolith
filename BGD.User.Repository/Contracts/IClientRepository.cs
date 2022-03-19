using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IClientRepository
    {
       Task<IEnumerable<Entities.Client>> GetAsync();
       Task<object> InsertAsync(Entities.Client client);
       Task<IEnumerable<Entities.Client>> FindAsync(Guid id);
       Task<int> DeleteAsync(Guid id);
       Task<Entities.Client> UpdateAsync(Entities.Client client);
       Task<dynamic> GetOrders(Entities.Client client);
    }
}