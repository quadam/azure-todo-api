using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.RequestProcessors
{
    public interface IGetTodosRequestProcessor
    {
        Task<PagedDataResponse<TodoModel>> ProcessGetRequest(HttpRequest httpRequest);
    }
}
