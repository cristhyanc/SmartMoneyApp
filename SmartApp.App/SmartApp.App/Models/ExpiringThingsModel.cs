using SmartApp.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartApp.App.Models
{
    public class ExpiringThingsModel
    {
        public ExpiryngThingDto Data { get; set; }

        public Color StatusColor
        {
            get
            {
                if (Data != null)
                {
                    if (Data.ExpireDate > DateTime.Now)
                    {
                        var totalDays = Data.ExpireDate - Data.CreatedOn.Value;
                        var daysLeft = Data.ExpireDate - DateTime.Now;

                        var percentage = (daysLeft.Days * 100) / (totalDays.Days==0?1: totalDays.Days);

                        if (percentage > 19)
                        {
                            return Color.White;
                        }
                        else if (percentage > 15 && percentage < 20)
                        {
                            return Color.FromHex("#f2e0aa");
                        }
                        else if (percentage > 10 && percentage < 15)
                        {
                            return Color.FromHex("#ffcf3d");
                        }
                    }
                    return Color.FromHex("#ff7e3d");
                }

                return Color.White;

            }

        }
    }
}
