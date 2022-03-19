using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IOrderItemRepository _repository;
        public OrderItemServices(IOrderItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Entities.OrderItem>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.OrderItem orderItem)
        {
            if (orderItem.OrdersId == null || orderItem.ItemsId == null)
            {
                throw new Exception("NOT_NULL_CLIENT_ID");
            }
            var result = await _repository.InsertAsync(orderItem);
            
            return orderItem;
        }

        public async Task<bool> InsertMany(IEnumerable<Entities.OrderItem> orderItem)
        {
            foreach (var entity in orderItem)
            {
                if (entity.OrdersId == null || entity.ItemsId == null)
                {
                    throw new Exception("NOT_NULL_CLIENT_ID");
                }
                await _repository.InsertAsync(entity);
            }

            return true;
        }

        public async Task<Entities.OrderItem> Get(Guid id)
        {
            var orderUserRepository = await _repository.FindAsync(id);
            
            if (orderUserRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return orderUserRepository.FirstOrDefault();
        }

        public async Task<int> Delete(Guid id)
        {
            var orderUserRepository = await _repository.FindAsync(id);

            if (orderUserRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.OrderItem> Put(Entities.OrderItem orderItem)
        {
            var orderRepository = await _repository.FindAsync(orderItem.Id.Value);

            if (orderRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(orderItem);
        }
    }
}