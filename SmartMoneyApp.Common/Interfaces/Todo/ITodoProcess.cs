using SmartApp.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Common.Interfaces.Todo
{
    public interface ITodoProcess
    {
         IEnumerable<TodoItem> GetTodos();
    }
}
