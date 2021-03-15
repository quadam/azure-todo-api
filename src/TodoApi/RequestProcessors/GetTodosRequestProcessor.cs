using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.RequestProcessors
{
    public class GetTodosRequestProcessor : IGetTodosRequestProcessor
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodosRequestProcessor(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<PagedDataResponse<TodoModel>> ProcessGetRequest(HttpRequest httpRequest)
        {
            var count = await _todoRepository.CountTodoItems();
            var pageSize = 20;
            var totalPages = count / pageSize + 1;
            var page = Convert.ToInt64(httpRequest.Query["page"]);

            var todoEntitiesList = await _todoRepository.ListTodoItems(page * pageSize, pageSize);

            var todoModelsList = new List<TodoModel>();

            foreach (var entity in todoEntitiesList)
            {
                var todoModel = new TodoModel
                {
                    Completed = entity.Completed,
                    Content = entity.Content,
                    Group = entity.PartitionKey,
                    Id = entity.RowKey
                };

                todoModelsList.Add(todoModel);
            }

            return new PagedDataResponse<TodoModel>()
            {
                Data = todoModelsList,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = count,
            };
        }
    }
}
