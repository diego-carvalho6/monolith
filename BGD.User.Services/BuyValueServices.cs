using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class BuyValueServices : IBuyValueServices
    {
        private readonly IBuyValueRepository _repository;
        public BuyValueServices(IBuyValueRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Entities.BuyValue>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.BuyValue buyValue)
        {
            if (string.IsNullOrEmpty(buyValue.Category))
            {
                throw new Exception("NOT_NULL_VALUES");
            }

            var buyValuesRepository = await _repository.GetAsync();
            
            if (buyValuesRepository.Any(x => x.Category.Equals(buyValue.Category)))
            {
                throw new Exception("CATEGORY_IN_USE");
            }
            
            var result = await _repository.InsertAsync(buyValue);
            
            return buyValue;
        }

        public async Task<Entities.BuyValue> Get(Guid id)
        {
            var buyValueRepository = await _repository.FindAsync(id);
            
            if (buyValueRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return buyValueRepository.FirstOrDefault();
        }

        public async Task<int> Delete(Guid id)
        {
            var buyValueRepository = await _repository.FindAsync(id);

            if (buyValueRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.BuyValue> Put(Entities.BuyValue buyValue)
        {
            var buyValueRepository = await _repository.FindAsync(buyValue.Id.Value);

            if (buyValueRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            if (buyValueRepository.FirstOrDefault().Category != buyValue.Category)
            {
                var buyValuesRepository = await _repository.GetAsync();
            
                if (buyValuesRepository.Any(x => x.Category.Equals(buyValue.Category)))
                {
                    throw new Exception("CATEGORY_IN_USE");
                }
            }
            
            return await _repository.UpdateAsync(buyValue);
        }
    }

}