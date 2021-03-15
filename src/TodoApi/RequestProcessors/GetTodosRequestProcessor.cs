using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Repositories;
using AutoMapper;

namespace TodoApi.RequestProcessors
{
    public class GetTodosRequestProcessor : IGetTodosRequestProcessor
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodosRequestProcessor(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<PagedDataResponse<TodoModel>> ProcessGetRequest(HttpRequest httpRequest)
        {
            var count = await _todoRepository.CountTodoItems();
            var pageSize = 20;
            var totalPages = count / pageSize + ((count % pageSize > 0) ? 1 : 0);
            var page = Convert.ToInt64(httpRequest.Query["page"]);

            var todoEntitiesList = await _todoRepository.ListTodoItems(page * pageSize, pageSize);

            var todoModelsList = _mapper.Map<List<TodoModel>>(todoEntitiesList);

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
