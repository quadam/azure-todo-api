using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Entities;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        Task<List<TodoEntity>> ListTodoItems(long start, long count);
        Task<long> CountTodoItems();
        Task CreateTodoItem(TodoEntity todoEntity);
    }
}
