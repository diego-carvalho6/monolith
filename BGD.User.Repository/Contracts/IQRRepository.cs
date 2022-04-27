using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IQRRepository
    {
        Task<IEnumerable<Entities.QR>> GetAsync();
        Task<object> InsertAsync(Entities.QR code);
        Task<object> InsertAdminAsync(Entities.QR code, string tenant = null);
        Task<IEnumerable<Entities.QR>> FindAsync(string id);
        Task<int> DeleteAsync(string id);
        Task<Entities.QR> UpdateAsync(Entities.QR code);
    }
}