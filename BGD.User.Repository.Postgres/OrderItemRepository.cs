using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IPostgresRepository<Entities.OrderItem> _repository;
        public OrderItemRepository(IPostgresRepository<Entities.OrderItem> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.OrderItem>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.OrderItem orderItem) => await _repository.CreateAsync(orderItem);
        public async Task<IEnumerable<Entities.OrderItem>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.OrderItem> UpdateAsync(Entities.OrderItem orderItem) => await _repository.UpdateAsync(orderItem, new {Id = orderItem.Id});
    }
}