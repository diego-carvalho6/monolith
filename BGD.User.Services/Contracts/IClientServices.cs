using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IClientServices
    {
        Task<IEnumerable<Entities.Client>> GetAll();
        Task<object> Insert(Entities.Client client);
        Task<Entities.Client> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.Client> Put(Entities.Client client);
    }
}