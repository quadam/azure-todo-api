using AutoMapper;
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
        private readonly IMapper _mapper;

        public PostTodoRequestProcessor(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task ProcessTodoRequest(HttpRequest httpRequest)
        {
            string requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();

            var todoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TodoModel>(requestBody);

            var todoEntity = _mapper.Map<TodoEntity>(todoModel);

            todoEntity.RowKey = Guid.NewGuid().ToString();

            await _todoRepository.CreateTodoItem(todoEntity);
        }
    }
}
