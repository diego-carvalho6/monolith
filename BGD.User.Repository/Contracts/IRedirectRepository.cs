using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IRedirectRepository
    {
        Task<IEnumerable<Entities.QR>> FindAsync(string id, string tenant);
    }
}