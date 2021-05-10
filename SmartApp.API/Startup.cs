using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartApp.Common.Interfaces.Todo;
using SmartApp.Core.Contract;
using SmartApp.Core.Services;
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
            builder.Services.AddDbContext<SmartAppContext>(
                options => options.UseSqlServer(SqlConnection));

            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
