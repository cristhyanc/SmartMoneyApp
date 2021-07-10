using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.API.Model
{
    public class ExpiringThingsPostResquet
    {
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Renew { get; set; }
    }
}
