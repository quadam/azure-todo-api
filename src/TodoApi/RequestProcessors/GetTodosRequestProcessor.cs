using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.RequestProcessors
{
    public class GetTodosRequestProcessor : IGetTodosRequestProcessor
    {
        public async Task<PagedDataResponse<TodoModel>> ProcessGetRequest(HttpRequest httpRequest)
        {
            return new PagedDataResponse<TodoModel>()
            {
                Data = new List<TodoModel>(),
                Page = 0,
                PageSize = 1,
                TotalPages = 2,
                TotalCount = 3,
            };
        }
    }
}
