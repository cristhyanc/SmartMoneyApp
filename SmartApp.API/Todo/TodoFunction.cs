//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using SmartApp.Core.Contract;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using System.Net;
//using Microsoft.OpenApi.Models;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
//using SmartApp.Common.DTO;

//namespace SmartApp.API.Todo
//{
//    public class TodoFunction
//    {

//        private readonly ITodoService _service;
//        public TodoFunction(ITodoService context)
//        {
//            _service = context;
//        }





//        //[FunctionName("GetTodos")]
//        //public  async Task<IActionResult> GetTodos(
//        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo")] HttpRequest req,
//        //   ILogger log)
//        //{
//        //    try
//        //    {
//        //        log.LogInformation("C# HTTP trigger function processed a request.");                

//        //        var list2 = await _service.GetAllTodos();
//        //        return new JsonResult(list2);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        throw ex;
//        //    }
           
//        //}

//        [FunctionName("GetTodo")]      
//        [OpenApiOperation(operationId: "GetTodo", tags: new[] { "id" })]
//       // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
//        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Int64), Description = "Todo Id")]
//        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TodoItem), Description = "Todo details")]
//        public async Task<IActionResult> GetTodo(
//           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo/{id}")] HttpRequest req,
//          ILogger log, Int64 id)
//        {
//            try
//            {
//                log.LogInformation("C# HTTP trigger function processed a request.");

               

//                var todo = await _service.GetTodo(id);
//                if(todo==null)
//                {
//                    return new NotFoundResult();
//                }
              
//                return new OkObjectResult(todo);
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }
//    }
//}
