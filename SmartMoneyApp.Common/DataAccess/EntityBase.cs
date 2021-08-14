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
              
        public string UserId { get; set; }
    }
}
