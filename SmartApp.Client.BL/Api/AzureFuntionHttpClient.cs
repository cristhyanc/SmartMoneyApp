using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SmartApp.Client.BL.Api
{
    public class AzureFuntionHttpClient: HttpClient
    {
        public string BaseApiServiceUrl { get; set; }
    

        public AzureFuntionHttpClient(string baseApiServiceUrl)
        {
            BaseApiServiceUrl = baseApiServiceUrl;
           
        }
    }
}
