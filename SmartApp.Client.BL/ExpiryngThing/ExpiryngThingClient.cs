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


        public async Task<PagedResult<ExpiryngThingDto>> GetAllExpiringThings(int pageNo, int pageSize)
        {
            var result = await this.GetAsync<PagedResult<ExpiryngThingDto>>($"expiringthing/{pageNo}/{pageSize}");
            return result;
        }

        public async Task<ExpiryngThingDto> CreateExpiringThings(ExpiryngThingDto data)
        {
            var result = await this.PostAsync<ExpiryngThingDto, ExpiryngThingDto>($"expiringthing", data);
            return result;
        }

        public async Task<ExpiryngThingDto> UpdateExpiringThings(long expiringThingId, ExpiryngThingDto data)
        {
            var result = await this.PutAsync<ExpiryngThingDto, ExpiryngThingDto>($"expiringthing/{expiringThingId}", data);
            return result;
        }

        public async Task DeleteExpiringThings(long expiringThingId)
        {
            await this.DeleteAsync($"expiringthing/{expiringThingId}");           
        }
    }
}
