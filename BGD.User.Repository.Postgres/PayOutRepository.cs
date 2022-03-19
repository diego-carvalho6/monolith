using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class PayOutRepository : IPayOutRepository
    {
            private readonly IPostgresRepository<Entities.PayOut> _repository;
            public PayOutRepository(IPostgresRepository<Entities.PayOut> repository)
            {
                _repository = repository;  
            }
            public async Task<IEnumerable<Entities.PayOut>> GetAsync() => await _repository.GetAsync();
            public async Task<object> InsertAsync(Entities.PayOut payOut) => await _repository.CreateAsync(payOut);
            public async Task<IEnumerable<Entities.PayOut>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
            public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
            public async Task<Entities.PayOut> UpdateAsync(Entities.PayOut payOut) => await _repository.UpdateAsync(payOut, new {Id = payOut.Id});
        
    }
}