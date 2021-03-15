using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Entities;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.RequestProcessors
{
    public class PostTodoRequestProcessor : IPostTodoRequestProcessor
    {
        private readonly ITodoRepository _todoRepository;

        public PostTodoRequestProcessor(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task ProcessTodoRequest(HttpRequest httpRequest)
        {
            string requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();

            var todoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TodoModel>(requestBody);

            var todoEntity = new TodoEntity
            {
                Completed = todoModel.Completed,
                Content = todoModel.Content,
                PartitionKey = todoModel.Group,
                RowKey = Guid.NewGuid().ToString(),
            };

            await _todoRepository.CreateTodoItem(todoEntity);
        }
    }
}
