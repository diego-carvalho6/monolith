using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class OrderUserServices : IOrderUserServices
    {
        private readonly IOrderUserRepository _repository;
        public OrderUserServices(IOrderUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Entities.OrderUser>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.OrderUser orderUser)
        {
            if (orderUser.OrdersId == null || orderUser.UsersId == null)
            {
                throw new Exception("NOT_NULL_CLIENT_ID");
            }
            var result = await _repository.InsertAsync(orderUser);
            
            return orderUser;
        }

        public async Task<Entities.OrderUser> Get(Guid id)
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

        public async Task<Entities.OrderUser> Put(Entities.OrderUser orderUser)
        {
            var orderRepository = await _repository.FindAsync(orderUser.Id.Value);

            if (orderRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(orderUser);
        }
    }
}