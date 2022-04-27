using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Npgsql;
using BGD.User.Repository.Dapper.Postgres.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.Repository.Postgres.Connection
{
    public class PostgresConnectionFactory : IPostgresConnectionFactory
    {
        private readonly IHttpContextAccessor _acessor;
        private readonly IConfiguration _enviromentConfiguration;

        public PostgresConnectionFactory(IConfiguration enviromentConfiguration, IHttpContextAccessor acessor)
        {
            _enviromentConfiguration =  enviromentConfiguration;
            _acessor = acessor;
        }
        public NpgsqlConnection Connection(string tenant = null)
        {
            var claim = _acessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Tenant"));
            var defaultDatabase = _enviromentConfiguration.GetConnectionString("PostgresConnection");
            var databaseName = defaultDatabase.Split(";").Where(x => x.StartsWith("database")).FirstOrDefault();
            if (claim != null && !claim.Value.Equals("default"))
            {
                defaultDatabase = defaultDatabase.Replace(databaseName, $"database={claim.Value}");
            }

            if (tenant != null && !tenant.Equals("default"))
            {
                defaultDatabase = defaultDatabase.Replace(databaseName, $"database={tenant}");
            }
            
            
            return new NpgsqlConnection(defaultDatabase);
        }
    }
}
