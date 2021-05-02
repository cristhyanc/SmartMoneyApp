using System;
using System.Collections.Generic;
using System.Text;
using SmartApp.Common.Interfaces.Todo;
using SmartApp.Todo;
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

namespace SmartApp.API.Todo
{
    public class TodoFunction
    {

        private readonly ITodoProcess _context;
        public TodoFunction(ITodoProcess context)
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

                var list2 = _context.GetTodos();
                return new OkObjectResult(list2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}
