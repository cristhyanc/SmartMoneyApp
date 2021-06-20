using Newtonsoft.Json;
using SmartApp.Common.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Client.BL.Todo
{
    public class TodoApi : ClientAPI
    {
     

        public TodoApi(HttpClient http): base(http)
        {
           
        }

        public async Task<IList<TodoItem>> GetTodoList()
        {
            var result = await this.GetAsync<IList<TodoItem>>("todo");
            return result;
        }
    }
}
