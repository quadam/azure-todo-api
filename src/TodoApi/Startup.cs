using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Repositories;
using TodoApi.RequestProcessors;

[assembly: FunctionsStartup(typeof(TodoApi.Startup))]

namespace TodoApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IGetTodosRequestProcessor, GetTodosRequestProcessor>();
            builder.Services.AddScoped<IPostTodoRequestProcessor, PostTodoRequestProcessor>();

            string connectionString = Environment.GetEnvironmentVariable("TodoApi_AzureTableStorage_ConnectionString");

            builder.Services.AddScoped<ITodoRepository, TodoRepository>((s) =>
            {
                return new TodoRepository(connectionString);
            });

            builder.Services.AddAutoMapper(typeof(Startup));
        }
    }
}
