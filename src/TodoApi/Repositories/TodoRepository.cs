using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Entities;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        public Task<long> CountTodoItems()
        {
            return Task.FromResult((long)1);
        }

        public Task CreateTodoItem(TodoEntity todoEntity)
        {
            return Task.CompletedTask;
        }

        public Task<List<TodoEntity>> ListTodoItems(long start, long count)
        {
            return Task.FromResult(new List<TodoEntity>
            {
                new TodoEntity
                {
                    ETag = "Tag",
                    PartitionKey = "PK",
                    RowKey = "1",
                    Timestamp = DateTime.Now
                }
            });
        }
    }
}
