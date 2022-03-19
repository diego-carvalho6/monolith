using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IBuyValueServices
    {
        Task<IEnumerable<Entities.BuyValue>> GetAll();
        Task<object> Insert(Entities.BuyValue buyValue);
        Task<Entities.BuyValue> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.BuyValue> Put(Entities.BuyValue buyValue);
    }
}