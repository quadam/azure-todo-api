using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TodoApi.RequestProcessors
{
    public interface IPostTodoRequestProcessor
    {
        Task ProcessTodoRequest(HttpRequest httpRequest);
    }
}
