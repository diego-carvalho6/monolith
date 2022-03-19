using BGD.User.Repository.Dapper.Postgres.Contracts;
using System;
using System.Linq;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace BGD.User.Repository.Dapper.Postgres.Database
{
    public class PostgresDatabase
    {
        private readonly IPostgresConnectionFactory _connection;
        private readonly IConfiguration _enviromentConfiguration;

        public PostgresDatabase(IPostgresConnectionFactory connection, IConfiguration enviromentConfiguration)
        {
            _connection = connection;
            _enviromentConfiguration = enviromentConfiguration;
        }

        public bool CreateDatabase()
        {
            var connection = new NpgsqlConnection(_enviromentConfiguration.GetConnectionString("PostgresConnection"));
            var databases = _enviromentConfiguration.GetSection("Databases").AsEnumerable();

            foreach (var database in databases)
            {
                if (!(database.Value == null))
                {
                    var createDatabase = new NpgsqlCommand($@"
                    CREATE DATABASE {database.Value.Replace(" ", "")}
                    WITH OWNER = postgres
                    ENCODING = 'UTF8'
                    CONNECTION LIMIT = -1;
                    ", connection);

                    var defaultConnectionString = _enviromentConfiguration.GetConnectionString("PostgresConnection");

                    var defaultDatabase = defaultConnectionString.Split(";").Where(x => x.StartsWith("database")).FirstOrDefault();
                    
                    var serviceProvider = CreateServices(defaultConnectionString
                        .Replace(defaultDatabase, $"database={database.Value}"));
                    try
                    {
                        connection.Open();
                        createDatabase.ExecuteNonQuery();
                        connection.Close();
                        using (var provider = serviceProvider.CreateScope())
                        {
                            RunMigrations(provider.ServiceProvider);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("already exists"))
                        {
                            connection.Close();
                            using (var provider = serviceProvider.CreateScope())
                            {
                                RunMigrations(provider.ServiceProvider);
                            }
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
                else
                {
                    var serviceProvider = CreateServices(_enviromentConfiguration.GetConnectionString("PostgresConnection"));
                    using (var provider = serviceProvider.CreateScope())
                    {
                        RunMigrations(provider.ServiceProvider);
                    }
                }
            }
            return true;
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(c => 
                    c.AddPostgres11_0()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(ReferenceClass).Assembly).For.All())
                .AddLogging(x => x.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void RunMigrations(IServiceProvider service)
        {
            var migrationService = service.GetRequiredService<IMigrationRunner>();
            migrationService.ListMigrations();
            migrationService.MigrateUp();
            // migrationService.Rollback(1);
            
        }


    }
}
