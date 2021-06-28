using SmartApp.App.Models;
using SmartApp.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.App.ViewModels.ExpiringThings
{
   public class AddExpiringThingViewModel: BaseViewModel
    {

        private ExpiryngThingDto _item;
        public ExpiryngThingDto Item
        {
            get => _item;
            set
            {
                SetProperty(ref _item, value);             
            }
        }

        public AddExpiringThingViewModel()
        {
            Item = new ExpiryngThingDto();
            Item.ExpireDate = DateTime.Now;
        }
    }
}
