using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Entities;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;
using Microsoft.Azure.Cosmos.Table.Queryable;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly CloudTable _todoTable;

        public TodoRepository(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            _todoTable = tableClient.GetTableReference("Todo");
        }

        public Task<long> CountTodoItems()
        {
            // TODO Execute count in query.
            // TODO Make method async.
            TableQuery<TodoEntity> query = new TableQuery<TodoEntity>()
                .Select(new List<string> { "PartitionKey", "RowKey", "Timestamp" });
            var entities = _todoTable.ExecuteQuery(query).ToList();
            return Task.FromResult((long)entities.Count);
        }

        public async Task CreateTodoItem(TodoEntity todoEntity)
        {
            var operation = TableOperation.Insert(todoEntity);

            await _todoTable.ExecuteAsync(operation);
        }

        public async Task<List<TodoEntity>> ListTodoItems(long start, long count)
        {
            // TODO: Implement start and count in query.
            var query = _todoTable.CreateQuery<TodoEntity>();
            var nextQuery = query;
            TableContinuationToken continuationToken = null;
            var entities = new List<TodoEntity>();

            do
            {
                var queryResults = await nextQuery.ExecuteSegmentedAsync(continuationToken);

                entities.AddRange(queryResults.Results);

                continuationToken = queryResults.ContinuationToken;

                if (continuationToken != null && query.TakeCount.HasValue)
                {
                    var itemsToLoad = query.TakeCount.Value - entities.Count;

                    nextQuery = itemsToLoad > 0
                        ? query.Take(itemsToLoad).AsTableQuery() : null;
                }
            }
            while (continuationToken != null && query != null);

            return entities.Skip((int)start).Take((int)count).ToList();
        }
    }
}
