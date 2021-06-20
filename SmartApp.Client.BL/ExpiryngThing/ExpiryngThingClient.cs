using SmartApp.Client.BL.Api;
using SmartApp.Common.DTO;
using SmartApp.Common.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Client.BL.ExpiryngThing
{
    public class ExpiryngThingClient:  ClientAPI, IExpiryngThingClient
    {
        public ExpiryngThingClient(AzureFuntionHttpClient http) : base(http)
        {

        }


        public async Task<PagedResult<ExpiryngThingDto>> GetAllExpiryngThings(int pageNo, int pageSize)
        {
            var result = await this.GetAsync<PagedResult<ExpiryngThingDto>>($"expiringthing/{pageNo}/{pageSize}");
            return result;

        }

    }
}
