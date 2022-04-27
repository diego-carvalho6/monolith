using System;
using BGD.User.Repository.Dapper.Postgres.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dommel;


namespace BGD.User.Repository.Dapper.Postgres
{
    public class PostgresRepository<TEntity> : IPostgresRepository<TEntity> where TEntity : class, new()
    {
        private readonly IPostgresConnectionFactory _connection;
        public PostgresRepository(IPostgresConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<TEntity>> GetAsync(string tenant = null)
        {
            using (var connection = _connection.Connection(tenant))
            {
                return await connection.GetAllAsync<TEntity>();
            }
        }

        public async Task<object> CreateAsync(TEntity Entity, string tenant = null)
        {
            using (var connection = _connection.Connection(tenant))
            {
                return await connection.InsertAsync(Entity);
            }
        }

        public async Task<IEnumerable<TEntity>> FindAsync(object id, string tenant = null)
        {
            var charString = '\u0022';
            var entityName = new TEntity().GetType().Name;
            var tableName = string.Concat((entityName ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(entityName[i-1]) ? $"_{x}" : x.ToString())).ToLower();
            var query = @"SELECT * FROM " + charString + tableName + charString + " WHERE " + charString + "Id" + charString + " = @Id";
            using (var connection = _connection.Connection(tenant))
            {
                return await connection.QueryAsync<TEntity>(query, id);
            }
        }
        public async Task<IEnumerable<dynamic>> GetQueryAsync(string query, object? parameters, string tenant = null) => await Task.Run(() => _connection.Connection(tenant).QueryAsync(query, parameters));

        public async Task<int> DeleteAsync(object id, string tenant = null)
        {
            var charString = '\u0022';
            var entityName = new TEntity().GetType().Name;
            var tableName = string.Concat((entityName ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(entityName[i-1]) ? $"_{x}" : x.ToString())).ToLower();
            var query = @"Delete FROM " + tableName + " WHERE " + charString + "Id" + charString + " = @Id";
            using (var connection = _connection.Connection(tenant))
            {
                return await connection.ExecuteAsync(query, id);
            }
        }
        
        public async Task<int> ExecuteQueryAsync(string query, object parameters, string tenant = null)
        {
            using (var connection = _connection.Connection(tenant))
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, object id, string tenant = null)
        {
            var charString = '\u0022';
            var singleCote = '\u0027';
            var entityName = new TEntity().GetType().Name;
            var tableName = string.Concat((entityName ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(entityName[i-1]) ? $"_{x}" : x.ToString())).ToLower();
            var property = entity.GetType().GetProperties();
            // var newEntity = new object();
            //
            // foreach (var propertyInfo in property)
            // {
            //     newEntity = 
            //     newEntity.GetType().GetProperties().SetValue(); 
            // }
            // var filterEntityName = property.Where(x => x.GetValue(entity) != null).Select(x => x.Name);
            // var filterEntityValue = property.Where(x => x.GetValue(entity) != null).Select(x => x.GetValue(entity));
            var filterEntity = property.Where(x =>  !x.PropertyType.IsArray && x.GetValue(entity) != null );
            var columns = string.Join(", ", filterEntity.Select(x => charString + x.Name + charString));
            var values = string.Join(", ", filterEntity.Select(x => x.PropertyType.IsEnum ? singleCote + ((int)x.GetValue(entity)).ToString() + singleCote : x.PropertyType.Name.Equals("Decimal") ? singleCote + x.GetValue(entity).ToString().Replace(",", ".") + singleCote : singleCote + x.GetValue(entity)?.ToString() + singleCote));
            var query = @"UPDATE " + charString + tableName + charString + " SET (" + columns + ") = (" + values + ") WHERE " + charString + "Id" + charString + " = @Id";

            using (var connection = _connection.Connection(tenant))
            {
                await connection.ExecuteScalarAsync<TEntity>(query, id);
                return entity;
            }
        }
    }
}
