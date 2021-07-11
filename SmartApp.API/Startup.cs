using MapsterMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartApp.Common.Interfaces.Todo;
using SmartApp.Core.Contract;
using SmartApp.Core.Contract.ExpiringThings;
using SmartApp.Core.Services;
using SmartApp.Core.Services.ExpiringThings;
using SmartApp.DataAccess;
using SmartApp.DataAccess.Repositories;
using System;


[assembly: FunctionsStartup(typeof(SmartApp.API.Startup))]

namespace SmartApp.API
{

   public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            string appInsights = Environment.GetEnvironmentVariable("appInsights");

            builder.Services.AddDbContext<SmartAppContext>(
                options => options.UseSqlServer(SqlConnection));

            //builder.Services.AddScoped<ITodoService, TodoService>();
           // builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<IExpiryngThingService, ExpiryngThingService>();
            builder.Services.AddScoped<IExpiryngThingRepository, ExpiryngThingRepository>();
            //builder.Services.AddApplicationInsightsTelemetry(appInsights);
            
        }
    }
}

