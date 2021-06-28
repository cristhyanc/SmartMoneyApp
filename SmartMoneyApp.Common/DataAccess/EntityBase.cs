using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Common.DataAccess
{
   public class EntityBase
    {
        public EntityBase()
        {
            this.CreatedOn = DateTime.Now;
        }
        public DateTime CreatedOn { get; set; }

        public Guid TenantId { get; set; }
    }
}
