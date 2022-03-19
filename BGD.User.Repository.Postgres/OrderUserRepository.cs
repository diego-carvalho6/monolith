using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class OrderUserRepository : IOrderUserRepository
    {
        private readonly IPostgresRepository<Entities.OrderUser> _repository;
        public OrderUserRepository(IPostgresRepository<Entities.OrderUser> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.OrderUser>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.OrderUser orderUser) => await _repository.CreateAsync(orderUser);
        public async Task<IEnumerable<Entities.OrderUser>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.OrderUser> UpdateAsync(Entities.OrderUser orderUser) => await _repository.UpdateAsync(orderUser, new {Id = orderUser.Id});
    }
}