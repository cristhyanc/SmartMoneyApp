using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Common.DTO
{
    public class TodoItem
    {
        public Int64 Id { get; set; }
        public string Description { get; set; }

        public DateTime DueDate { get; set; }
    }
}
