using Npgsql;

namespace BGD.User.Repository.Dapper.Postgres.Contracts
{
    public interface IPostgresConnectionFactory
    {
        NpgsqlConnection Connection(string tenant = null);
    }
}
