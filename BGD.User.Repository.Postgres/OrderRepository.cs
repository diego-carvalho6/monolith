using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IPostgresRepository<Entities.Order> _repository;
        public OrderRepository(IPostgresRepository<Entities.Order> repository)
        {
            _repository = repository;  
        }
        
        public async Task<IEnumerable<Entities.Order>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.Order order) => await _repository.CreateAsync(order);
        public async Task<IEnumerable<Entities.Order>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.Order> UpdateAsync(Entities.Order order) => await _repository.UpdateAsync(order, new {Id = order.Id});

        public async Task<dynamic> VerifyOrder(VerifyOrderQuery verify)
        {
            var charString = '\u0022';
            var query = @"SELECT * FROM " + charString + "order" + charString + " WHERE " + charString + "Table" + charString + " = @Table AND " + charString + "Finished" + charString + "= @Finished";
            var parameters = new {verify.Table, verify.Finished};
            var result = await _repository.GetQueryAsync(query, parameters);

            return result;
        }
        public async Task<dynamic> GetUsers(Guid id)
        {
            var returnList = new List<object>();
            var charString = '\u0022';
            var query = @"SELECT " + charString + "UsersId" + charString + " FROM " + charString + "order_user" + charString + " WHERE " + charString + "OrdersId" + charString + " = @Id";
            var list = await _repository.GetQueryAsync(query, new {Id = id});
            foreach (var user in list)
            {
                query = @"SELECT * FROM " + charString + "user" + charString + " WHERE " + charString + "Id" + charString + "= @usersId";
                returnList.Add(await _repository.GetQueryAsync(query, new {usersId = user.UsersId}));
            }
            return returnList;
        }
        public async Task<dynamic> GetToDoList(Guid id)
        {
            var charString = '\u0022';
            var query = "SELECT * FROM to_do_list WHERE " + charString + "Orderid" + charString + " = @Id" ;
            var parameters = new {Id = id};
            return await _repository.GetQueryAsync(query, parameters);
        }
        public async Task<dynamic> GetPayOuts(Guid id)
        {
            var charString = '\u0022';
            var query = "SELECT * FROM pay_out WHERE " + charString + "Orderid" + charString + " = @Id" ;
            var parameters = new {Id = id};
            return await _repository.GetQueryAsync(query, parameters);
        }
        public async Task<dynamic> GetItems(Guid id)
        {
            var returnList = new List<object>();
            var charString = '\u0022';
            var query = @"SELECT " + charString + "ItemsId" + charString + " FROM " + charString + "order_item" + charString + " WHERE " + charString + "OrdersId" + charString + " = @Id";
            var list = await _repository.GetQueryAsync(query, new {Id = id});
            foreach (var item in list)
            {
                query = @"SELECT * FROM " + charString + "item" + charString + " WHERE " + charString + "Id" + charString + "= @itemsId";
                returnList.Add(await _repository.GetQueryAsync(query, new {itemsId = item.ItemsId}));
            }
            return returnList;
        }
        
        
    }
}