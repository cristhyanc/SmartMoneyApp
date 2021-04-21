using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SmartMoneyApp.API.Todo
{
    public static class TodoFunction
    {
        [FunctionName("GetTodos")]
        public static async Task<IActionResult> GetTodos(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "todo")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");           

            string responseMessage = "list of todos";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("UpdateTodo")]
        public static async Task<IActionResult> UpdateTodo(
           [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "todo/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
           
            string responseMessage = "todo updated" + id;

            return new OkObjectResult(responseMessage);
        }

    }
   
}

