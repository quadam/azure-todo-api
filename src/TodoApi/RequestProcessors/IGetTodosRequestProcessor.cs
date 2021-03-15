using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.RequestProcessors
{
    public interface IGetTodosRequestProcessor
    {
        Task<PagedDataResponse<TodoModel>> ProcessGetRequest(HttpRequest httpRequest);
    }
}
