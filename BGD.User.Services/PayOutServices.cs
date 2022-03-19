using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class PayOutServices : IPayOutServices
    {
        private readonly IPayOutRepository _repository;
        public PayOutServices(IPayOutRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Entities.PayOut>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.PayOut payOut)
        {
            if (payOut.Orderid == null)
            {
                throw new Exception("NOT_NULL_ORDER_ID");
            }
            var result = await _repository.InsertAsync(payOut);
            
            return payOut;
        }

        public async Task<Entities.PayOut> Get(Guid id)
        {
            var payOutRepository = await _repository.FindAsync(id);
            
            if (payOutRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return payOutRepository.FirstOrDefault();
        }

        public async Task<int> Delete(Guid id)
        {
            var payOutRepository = await _repository.FindAsync(id);

            if (payOutRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.PayOut> Put(Entities.PayOut payOut)
        {
            var payOutRepository = await _repository.FindAsync(payOut.Id.Value);

            if (payOutRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(payOut);
        }
    }
}