using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IQRServices
    {
        Task<IEnumerable<Entities.QR>> GetAll();
        Task<object> Insert(Entities.QR code, ClaimsPrincipal user = null, string tenant = null);
        Task<Bitmap> Get(string id);
        Task<int> Delete(string id);
        Task<Entities.QR> Put(Entities.QR code);
    }
}