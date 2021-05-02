using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmartApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.API.Helper
{
    public class SmartAppContextFactory : IDesignTimeDbContextFactory<SmartAppContext>
    {
        public SmartAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SmartAppContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new SmartAppContext(optionsBuilder.Options);
        }
    }
}