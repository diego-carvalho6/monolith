using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;

namespace BGD.User.Repository.Postgres
{
    public class QRRepository : IQRRepository
    {
        private readonly IPostgresRepository<Entities.QR> _repository;
        public QRRepository(IPostgresRepository<Entities.QR> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.QR>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.QR code) => await _repository.CreateAsync(code);
        public async Task<object> InsertAdminAsync(Entities.QR code, string tenant) => await _repository.CreateAsync(code, tenant);
        public async Task<IEnumerable<Entities.QR>> FindAsync(string id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(string id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.QR> UpdateAsync(Entities.QR code) => await _repository.UpdateAsync(code, new {Id = code.Id});
    }
}