using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BGD.User.Services
{
    public class QRServices : IQRServices
    {
          private readonly IQRRepository _repository;
          private readonly IHttpContextAccessor _acessor;
          public QRServices(IQRRepository repository, IHttpContextAccessor acessor)
          {
              _repository = repository;
              _acessor = acessor;
          }
          public async Task<IEnumerable<Entities.QR>> GetAll() => await _repository.GetAsync();
          public async Task<object> Insert(Entities.QR code,ClaimsPrincipal user = null,  string tenant = null)
          {
              if (tenant != null)
              {
                  code.Tenant = tenant;
                  var resultAdmin = await _repository.InsertAdminAsync(code, tenant);
                  return code;
              }

              code.Tenant = user.Claims.FirstOrDefault(x => x.Type.Equals("Tenant")).Value;
              
              var result = await _repository.InsertAsync(code);
              return code;
          }
  
          public async Task<Bitmap> Get(string id)
          {
              var qRRepository = await _repository.FindAsync(id);
              
              if (qRRepository.Count() == 0)
              {
                  throw new NotFoundException();
              }
              var bitMap = qRRepository.FirstOrDefault().QRGenerator();
              return bitMap;
          }
  
          public async Task<int> Delete(string id)
          {
              var qRRepository = await _repository.FindAsync(id);
  
              if (qRRepository.Count() == 0)
              {
                  throw new NotFoundException();
              }
  
              return await _repository.DeleteAsync(id);
          }
  
          public async Task<Entities.QR> Put(Entities.QR code)
          {
              var payOutRepository = await _repository.FindAsync(code.Id);
  
              if (payOutRepository.Count() == 0)
              {
                  throw new NotFoundException();
              }
              
              return await _repository.UpdateAsync(code);
          }
      }
  }