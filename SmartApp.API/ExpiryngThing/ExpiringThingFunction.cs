using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SmartApp.API.Helper;
using SmartApp.API.Model;
using SmartApp.Common.DTO;
using SmartApp.Core.Contract;

namespace SmartApp.API.ExpiryngThing
{
    public  class ExpiringThingFunction
    {

        readonly IExpiryngThingService _expiryngThingService;

        public ExpiringThingFunction(IExpiryngThingService expiringThingService)
        {
            _expiryngThingService = expiringThingService;
        }



        [FunctionName("GetExpiringThings")]
        [OpenApiOperation(operationId: "GetExpiringThings", tags: new[] { "ExpiringThing" })]
        //  [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "pageno", In = ParameterLocation.Query, Required = false, Type = typeof(Int64), Description = "pageno")]
        [OpenApiParameter(name: "pagesize", In = ParameterLocation.Query, Required = false, Type = typeof(Int64), Description = "pagesize")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PagedResult<Common.DTO.ExpiryngThingDto>), Description = "Expiring things list")]
        public  async Task<IActionResult> GetExpiringThings(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "expiringthing/{pageno}/{pagesize}")] HttpRequest req, int pageno, int pagesize,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if(pagesize==0)
            {
                pagesize = 50;
            }

            var data = await _expiryngThingService.GetAll(pageno, pagesize);
            var result =new PagedResult<Common.DTO.ExpiryngThingDto>(data.Item2, pageno, data.Item2.Count(), data.Item1);
            return  new OkObjectResult(result);


        }

        [FunctionName("GetExpiringThing")]
        [OpenApiOperation(operationId: "GetExpiringThing", tags: new[] { "ExpiringThing" })]
        // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Int64), Description = "Expiringthing Id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Common.DTO.ExpiryngThingDto), Description = "Expiring thing")]
        public async Task<IActionResult> GetExpiringThing(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "expiringthing/{id}")] HttpRequest req,
           string id, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            long intId = 0;
            long.TryParse(id, out intId);

            var result = await _expiryngThingService.Get(intId);
            return new OkObjectResult(result);


        }

        [FunctionName("CreateExpiringThing")]
        [OpenApiOperation(operationId: "CreateExpiringThing", tags: new[] { "ExpiringThing" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExpiryngThingDto), Description = "Create Expiring thing")]
        public async Task<IActionResult> CreateExpiringThing(
         [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "expiringthing")] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = String.Empty;
                using (StreamReader streamReader = new StreamReader(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }

                var data = JsonConvert.DeserializeObject<ExpiringThingsPostResquet>(requestBody, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
                if (data == null)
                {
                    return new BadRequestObjectResult("Data required");
                }
                var newItem = new ExpiryngThingDto { Description = data.Description, ExpireDate = data.ExpireDate, Renew = data.Repeated };
                var result = await _expiryngThingService.Save(newItem);
                return new CreatedResult(result.Id.ToString(), result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}

