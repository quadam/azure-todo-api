using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TodoApi.RequestProcessors;

namespace TodoApi
{
    public class TodoFunction
    {
        private readonly IGetTodosRequestProcessor _getTodosRequestProcessor;
        private readonly IPostTodoRequestProcessor _postTodoRequestProcessor;

        public TodoFunction(IGetTodosRequestProcessor getTodosRequestProcessor, IPostTodoRequestProcessor postTodoRequestProcessor)
        {
            _getTodosRequestProcessor = getTodosRequestProcessor;
            _postTodoRequestProcessor = postTodoRequestProcessor;
        }

        [FunctionName("Todo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (string.Equals(req.Method, "get", StringComparison.OrdinalIgnoreCase))
            {
                var model = await _getTodosRequestProcessor.ProcessGetRequest(req);

                return new OkObjectResult(model);
            }
            else if (string.Equals(req.Method, "post", StringComparison.OrdinalIgnoreCase))
            {
                await _postTodoRequestProcessor.ProcessTodoRequest(req);

                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}

