using System;
using System.Collections.Generic;
using System.Text;
using SmartApp.Common.Interfaces.Todo;

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartApp.DataAccess;
using System.Linq;
using SmartApp.Core.Contract;

namespace SmartApp.API.Todo
{
    public class TodoFunction
    {

        private readonly ITodoService _context;
        public TodoFunction(ITodoService context)
        {
            _context = context;
        }





        [FunctionName("GetTodos")]
        public  async Task<IActionResult> GetTodos(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo")] HttpRequest req,
           ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string responseMessage = "list of todos";

                var list2 = await _context.GetAllTodos();
                return new OkObjectResult(list2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [FunctionName("GetTodo")]
        public async Task<IActionResult> GetTodo(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo/{id}")] HttpRequest req,
          ILogger log, Int64 id)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string responseMessage = "list of todos";

                var todo = await _context.GetTodo(id);
                if(todo==null)
                {
                    return new NotFoundResult();
                }
              
                return new OkObjectResult(todo);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
