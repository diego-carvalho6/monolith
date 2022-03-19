using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IPayOutServices
    {
        Task<IEnumerable<Entities.PayOut>> GetAll();
        Task<object> Insert(Entities.PayOut payOut);
        Task<Entities.PayOut> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.PayOut> Put(Entities.PayOut payOut);
    }
}