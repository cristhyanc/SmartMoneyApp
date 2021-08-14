using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SmartApp.Client.BL
{
    public abstract class ClientAPI
    {
        protected readonly HttpClient Http;      
        private readonly string _baseRoute;
        JsonSerializerSettings _deserializationSettings;

        protected ClientAPI(HttpClient http)
        {
            
            _baseRoute = http?.BaseAddress?.AbsoluteUri;
            Http = http;          

            _deserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,

            };
        }

        protected async Task<TReturn> GetAsync<TReturn>(string relativeUri)
        {
            HttpResponseMessage res = await Http.GetAsync($"{_baseRoute}/{relativeUri}");
            if (res.IsSuccessStatusCode)
            {
                if (res.StatusCode == HttpStatusCode.NoContent)
                    return default(TReturn);
                else
                    return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<TReturn> PostAsync<TReturn, TRequest>(string relativeUri, TRequest request)
        {
            HttpResponseMessage res = await Http.PostAsJsonAsync<TRequest>($"{_baseRoute}/{relativeUri}", request);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<TReturn> PutAsync<TReturn, TRequest>(string relativeUri, TRequest request)
        {
            HttpResponseMessage res = await Http.PutAsJsonAsync<TRequest>($"{_baseRoute}/{relativeUri}", request);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task DeleteAsync(string relativeUri)
        {
            HttpResponseMessage res = await Http.DeleteAsync($"{_baseRoute}/{relativeUri}");
            if (!res.IsSuccessStatusCode)
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }
    }
}
