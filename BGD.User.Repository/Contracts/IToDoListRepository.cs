using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGD.User.Repository.Contracts
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<Entities.ToDoList>> GetAsync();
        Task<object> InsertAsync(Entities.ToDoList toDoList);
        Task<IEnumerable<Entities.ToDoList>> FindAsync(Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<Entities.ToDoList> UpdateAsync(Entities.ToDoList toDoList);
    }
}