using System;
using System.Collections;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Entities.Extensions;
using Dapper;


namespace BGD.User.Repository.Postgres
{
    public class UserRepository : IUserRepository
    {
        private readonly IPostgresRepository<Entities.User> _repository;
        public UserRepository(IPostgresRepository<Entities.User> repository)
        {
            _repository = repository;  
        }

        public async Task<IEnumerable<Entities.User>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.User user) => await _repository.CreateAsync(user);
        public async Task<object> InsertAdminAsync(Entities.User user, Entities.Tenant tenant) => await _repository.CreateAsync(user, tenant.TenantName);

        public async Task<IEnumerable<Entities.User>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});

        public async Task<IEnumerable<dynamic>> QueryAsync(Entities.User user, Entities.Tenant tenant = null)
        {
            var charString = '\u0022';
            var parameters = new {Username = user.Username};
            var query = "SELECT * FROM " + charString + "user" + charString + " WHERE " + charString + "Username" + charString + " = @Username";
            return await _repository.GetQueryAsync(query, parameters, tenant?.TenantName);
        }
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});

        public async Task<Entities.User> UpdateAsync(Entities.User user) => await _repository.UpdateAsync(user, new {Id = user.Id});
        
        public async Task<IEnumerable<dynamic>> QueryOrdersAsync(Guid id)
        {
            var returnList = new List<object>();
            var charString = '\u0022';
            var parameters = new {Id = id};
            var query = @"SELECT " + charString + "OrdersId" + charString + " FROM " + charString + "order_user" + charString + " WHERE " + charString + "UsersId" + charString + " = @Id";
            var list = await _repository.GetQueryAsync(query, parameters);
            foreach (var order in list)
            {
                query = @"SELECT * FROM " + charString + "order" + charString + " WHERE " + charString + "Id" + charString + "= @orderId";
                returnList.Add(await _repository.GetQueryAsync(query, new {orderId = order.OrdersId}));
            }
            return returnList;
        }
        
    }
}
