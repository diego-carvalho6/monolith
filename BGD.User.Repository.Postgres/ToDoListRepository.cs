using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres.Contracts;


namespace BGD.User.Repository.Postgres
{
        
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly IPostgresRepository<Entities.ToDoList> _repository;
        public ToDoListRepository(IPostgresRepository<Entities.ToDoList> repository)
        {
            _repository = repository;  
        }
        public async Task<IEnumerable<Entities.ToDoList>> GetAsync() => await _repository.GetAsync();
        public async Task<object> InsertAsync(Entities.ToDoList toDoList) => await _repository.CreateAsync(toDoList);
        public async Task<IEnumerable<Entities.ToDoList>> FindAsync(Guid id) => await _repository.FindAsync(new {Id = id});
        public async Task<int> DeleteAsync(Guid id) => await _repository.DeleteAsync(new {Id = id});
        public async Task<Entities.ToDoList> UpdateAsync(Entities.ToDoList toDoList) => await _repository.UpdateAsync(toDoList, new {Id = toDoList.Id});
    }
}