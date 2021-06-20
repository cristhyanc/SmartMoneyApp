using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using SmartApp.Common.DTO;
using Newtonsoft.Json;


namespace SmartApp.Client.BL.Todo
{
   public class TodoProcess
    {
          
        public TodoProcess()
        {
           
        }

        public async Task LoadInfo()
        {
            try
            {
                var api = new TodoApi( new HttpClient());


                // var result = await Http.GetAsync("http://192.168.1.105/api/todo");
                //var result = await Http.GetStringAsync("http://192.168.1.105/api/todo");
                var result = await api.GetTodoList();

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
