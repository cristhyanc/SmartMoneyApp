using SmartApp.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Common.Interfaces.Client
{
    public interface IExpiryngThingClient
    {
        Task<PagedResult<ExpiryngThingDto>> GetAllExpiringThings(int pageNo, int pageSize);
        Task<ExpiryngThingDto> CreateExpiringThings(ExpiryngThingDto data);
        Task<ExpiryngThingDto> UpdateExpiringThings(long expiringThingId, ExpiryngThingDto data);
        Task DeleteExpiringThings(long expiringThingId);
    }
}
