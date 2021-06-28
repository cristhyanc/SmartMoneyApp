using Microsoft.EntityFrameworkCore;
using SmartApp.Core.Entities;
using SmartApp.Core.Entities.ExpiringThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.DataAccess
{
    public class SmartAppContext: DbContext
    {
        public  SmartAppContext(DbContextOptions options):base(options)
        {

        }

      //  public DbSet<TodoItem>  TodoItems { get; set; }
        public DbSet<ExpiryngThing> ExpiryngThing { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    try
        //    {
        //        modelBuilder.Entity<ExpiryngThing>().HasKey(x => x.Id);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
                    
        //}
    }
}
