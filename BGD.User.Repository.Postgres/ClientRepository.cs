using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class ClientRepository : IClientRepository
    {
        private readonly IPostgresRepository<Entities.Client> _repository;
        public ClientRepository(IPostgresRepository<Entities.Client> repository)
        {
            _repository = repository;  
        }
        
        public async Task<IEnumerable<Entities.Client>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.Client client) => await _repository.CreateAsync(client);
        public async Task<IEnumerable<Entities.Client>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.Client> UpdateAsync(Entities.Client client) => await _repository.UpdateAsync(client, new {Id = client.Id});

        public async Task<dynamic> GetOrders(Entities.Client client)
        {
            var charString = '\u0022';
            var query = "SELECT * FROM orders WHERE " + charString + "Clientid" + charString + " = @Id";
            var result = await _repository.GetQueryAsync(query, new {Id = client.Id});
            return result;
        }
    }
}