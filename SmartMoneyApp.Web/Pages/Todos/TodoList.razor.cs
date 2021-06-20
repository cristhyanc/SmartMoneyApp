using Microsoft.AspNetCore.Components;
using SmartApp.Common.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartMoneyApp.Web.Todo
{
    public partial class TodoList : ComponentBase
    {
        public IList<TodoItem> Todos { get; set; }

        [Inject]
        HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadInfo();
        }


        private async Task LoadInfo()
        {
            try
            {
                //var result = await Http.GetFromJsonAsync

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
