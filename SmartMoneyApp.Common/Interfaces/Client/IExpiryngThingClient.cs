using SmartApp.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Common.Interfaces.Client
{
    public interface IExpiryngThingClient
    {
        Task<PagedResult<ExpiryngThingDto>> GetAllExpiryngThings(int pageNo, int pageSize);
    }
}
