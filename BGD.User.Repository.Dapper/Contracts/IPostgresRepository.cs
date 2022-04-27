using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BGD.User.Repository.Dapper.Postgres.Contracts
{
    public interface IPostgresRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(string tenant = null);
        Task<object> CreateAsync(TEntity user, string tenant = null);

        public Task<IEnumerable<TEntity>> FindAsync(object id, string tenant = null);

        public Task<IEnumerable<dynamic>> GetQueryAsync(string query, object? parameters, string tenant = null);

        public Task<int> DeleteAsync(object id, string tenant = null);
        public Task<int> ExecuteQueryAsync(string query, object parameters, string tenant = null);

        public Task<TEntity> UpdateAsync(TEntity entity, object id, string tenant = null);

    }
}
