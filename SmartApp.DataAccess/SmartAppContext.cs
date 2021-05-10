using Microsoft.EntityFrameworkCore;
using SmartApp.Core.Entities;
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

        public DbSet<TodoItem>  TodoItems { get; set; }
    }
}
