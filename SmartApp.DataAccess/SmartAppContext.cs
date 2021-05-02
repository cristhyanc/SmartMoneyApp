using Microsoft.EntityFrameworkCore;
using SmartApp.DataAccess.Models.Todo;
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
