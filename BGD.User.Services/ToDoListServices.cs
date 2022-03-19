using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class ToDoListServices : IToDoListServices
    {
        private readonly IToDoListRepository _repository;
        public ToDoListServices(IToDoListRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Entities.ToDoList>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.ToDoList toDoList)
        {
            if (toDoList.Orderid == null)
            {
                throw new Exception("NOT_NULL_CLIENT_ID");
            }
            var result = await _repository.InsertAsync(toDoList);
            
            return toDoList;
        }

        public async Task<Entities.ToDoList> Get(Guid id)
        {
            var toDoListRepository = await _repository.FindAsync(id);
            
            if (toDoListRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return toDoListRepository.FirstOrDefault();
        }

        public async Task<int> Delete(Guid id)
        {
            var toDoListRepository = await _repository.FindAsync(id);

            if (toDoListRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.ToDoList> Put(Entities.ToDoList toDoList)
        {
            var toDoListRepository = await _repository.FindAsync(toDoList.Id.Value);

            if (toDoListRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(toDoList);
        }
    }
}