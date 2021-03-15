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
        private readonly IMyService _myService;
        private readonly IGetTodosRequestProcessor _getTodosRequestProcessor;

        public TodoFunction(IMyService myService, IGetTodosRequestProcessor getTodosRequestProcessor)
        {
            _myService = myService;
            _getTodosRequestProcessor = getTodosRequestProcessor;
        }

        [FunctionName("Todo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (string.Equals(req.Method, "get", StringComparison.OrdinalIgnoreCase))
            {
                var model = _getTodosRequestProcessor.ProcessGetRequest(req);

                return new OkObjectResult(model);
            }

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            responseMessage += " And my service returns: " + _myService.GetValue();

            return new OkObjectResult(responseMessage);
        }
    }
}

