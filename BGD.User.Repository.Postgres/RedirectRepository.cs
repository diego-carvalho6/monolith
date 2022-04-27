using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class RedirectRepository : IRedirectRepository
    {
        private readonly IPostgresRepository<Entities.QR> _repository;
        public RedirectRepository(IPostgresRepository<Entities.QR> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.QR>> FindAsync(string id, string tenant) => await _repository.FindAsync(new {Id = id});
    }
}