using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BGD.User.Repository.Dapper.Postgres.Database;
using BGD.User.Entities.Dapper;

namespace BGD.User.Repository.Dapper.Postgres.Migrations
{
    public static class PostgresMigrationsManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            MapperConfiguration.ConfigureMappers();
            var databaseService = scope.ServiceProvider.GetRequiredService<PostgresDatabase>();
            databaseService.CreateDatabase();
            return host;
        }

    }
}
