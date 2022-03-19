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
    public class ItemServices : IItemServices
    {
        private readonly IItemRepository _repository;
        public ItemServices(IItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Entities.Item>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.Item item) {
            
            var result = await _repository.InsertAsync(item);
            return item;
        }

        public async Task<Entities.Item> Get(Guid id)
        {
            var itemRepository = await _repository.FindAsync(id);
            
            if (itemRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            var item = itemRepository.FirstOrDefault();

            item.orders = new List<Order>();

            var ItemOrder = await _repository.GetOrders(id);
            
            foreach (var order in ItemOrder)
            {
                var newOrder = new Entities.Order();
                
                FillpropertiesExtension.Fillproperties(newOrder, order);
                
                item.orders.Add(newOrder);
            }
            
            return itemRepository.FirstOrDefault();
        }

        public async Task<int> Delete(Guid id)
        {
            var itemRepository = await _repository.FindAsync(id);

            if (itemRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.Item> Put(Entities.Item item)
        {
            var itemRepository = await _repository.FindAsync(item.Id.Value);

            if (itemRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(item);
        }
    }
}