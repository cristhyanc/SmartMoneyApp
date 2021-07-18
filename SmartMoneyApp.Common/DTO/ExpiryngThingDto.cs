using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Common.DTO
{
   public class ExpiryngThingDto
    {
        public Int64 Id { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid TenantId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
