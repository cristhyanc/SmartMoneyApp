using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Pipeline;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartMoneyApp.Common.DTO;
using SmartMoneyApp.Common.Interfaces.Todo;
using SmartMoneyApp.Todo;

namespace SmartMoneyApp.API.Todo
{
    public static class TodoFunction
    {
        [FunctionName("GetTodos")]
        public static async Task<IActionResult> GetTodos(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "todo")] HttpRequestData req,
            FunctionExecutionContext executionContext)
        {

            var log = executionContext.Logger;
            log.LogInformation("C# HTTP trigger function processed a request.");           

            string responseMessage = "list of todos";

            ITodoProcess todoProcess = new TodoProcess();
            var list = todoProcess.GetTodos();



            return new OkObjectResult(responseMessage);
        }

        //[FunctionName("UpdateTodo")]
        //public static async Task<IActionResult> UpdateTodo(
        //   [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "todo/{id}")] HttpRequest req,
        //   ILogger log, string id)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");
           
        //    string responseMessage = "todo updated" + id;

        //    return new OkObjectResult(responseMessage);
        //}

    }
   
}

