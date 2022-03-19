using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class BuyValueRepository : IBuyValueRepository
    {
        private readonly IPostgresRepository<Entities.BuyValue> _repository;
        public BuyValueRepository(IPostgresRepository<Entities.BuyValue> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.BuyValue>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.BuyValue buyValue) => await _repository.CreateAsync(buyValue);
        public async Task<IEnumerable<Entities.BuyValue>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.BuyValue> UpdateAsync(Entities.BuyValue buyValue) => await _repository.UpdateAsync(buyValue, new {Id = buyValue.Id});
    }
}