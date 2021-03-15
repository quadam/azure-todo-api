using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.RequestProcessors
{
    public interface IPostTodoRequestProcessor
    {
        Task ProcessTodoRequest(HttpRequest httpRequest);
    }
}
