using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class ItemRepository : IItemRepository
    {
        private readonly IPostgresRepository<Entities.Item> _repository;
        public ItemRepository(IPostgresRepository<Entities.Item> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.Item>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.Item item) => await _repository.CreateAsync(item);
        public async Task<IEnumerable<Entities.Item>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.Item> UpdateAsync(Entities.Item item) => await _repository.UpdateAsync(item, new {Id = item.Id});

        public async Task<dynamic> GetOrders(Guid id)
        {
            var returnList = new List<object>();
            var charString = '\u0022';
            var query = @"SELECT " + charString + "OrdersId" + charString + " FROM " + charString + "order_item" + charString + " WHERE " + charString + "ItemsId" + charString + " = @Id";
            var list = await _repository.GetQueryAsync(query, new {Id = id});
            foreach (var order in list)
            {
                query = @"SELECT * FROM " + charString + "order" + charString + " WHERE " + charString + "Id" + charString + "= @ordersId";
                returnList.Add(await _repository.GetQueryAsync(query, new {ordersId = order.OrdersId}));
            }

            return returnList;
        }
    }
}