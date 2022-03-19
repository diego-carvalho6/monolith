using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Services.Contracts
{
    public interface IToDoListServices
    {
        Task<IEnumerable<Entities.ToDoList>> GetAll();
        Task<object> Insert(Entities.ToDoList toDoList);
        Task<Entities.ToDoList> Get(Guid id);
        Task<int> Delete(Guid id);
        Task<Entities.ToDoList> Put(Entities.ToDoList toDoList);
    }
}