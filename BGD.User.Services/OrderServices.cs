using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Entities.Extensions;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _repository;
        public OrderServices(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Entities.Order>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.Order order)
        {
            
            var result = await _repository.InsertAsync(order);
            
            return order;
        }

        public async Task<Entities.Order> Get(Guid id)
        {
            var orderRepository = await _repository.FindAsync(id);
            
            if (orderRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            var order = orderRepository.FirstOrDefault();

            var orderUser = await _repository.GetUsers(id);
            var orderPayOut = await _repository.GetPayOuts(id);
            var orderItem = await _repository.GetItems(id);

            order.Employers = new List<Entities.User>();
            order.Payouted = new List<PayOut>();
            order.Items = new List<Item>();
            
            foreach (var user in orderUser)
            {
                var newUser = new Entities.User();
                
                FillpropertiesExtension.Fillproperties(newUser, user);
                
                order.Employers.Add(new Entities.User
                {
                    Id = newUser.Id,
                    Username = newUser.Username
                });
            }
            
            foreach (var payOut in orderPayOut)
            {
                order.Payouted.Add(new PayOut
                {
                    Id = payOut.Id,
                    Category = payOut.Category,
                    Value = payOut.Value
                });
            }
            

            foreach (var item in orderItem)
            {
                var newItem = new Item();
                
                FillpropertiesExtension.Fillproperties(newItem, item);
                
                order.Items.Add(newItem);
            }

            
            
            return order;
        }

        public async Task<Entities.Order> VerifyOrder(VerifyOrderQuery verify)
        {
            var result = await _repository.VerifyOrder(verify);

            if (result.Count == 0)
            {
                throw new Exception("NOT_FOUND");
            }

            var order = new Order();
            
            FillpropertiesExtension.Fillproperties(order, result);

            return order;
        }

        public async Task<int> Delete(Guid id)
        {
            var orderRepository = await _repository.FindAsync(id);

            if (orderRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.Order> Put(Entities.Order order)
        {
            var orderRepository = await _repository.FindAsync(order.Id.Value);

            if (orderRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(order);
        }
        
    }
}