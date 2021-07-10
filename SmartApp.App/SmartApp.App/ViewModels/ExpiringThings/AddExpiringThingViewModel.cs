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

        private bool _isValidationVisible;
        public bool IsValidationVisible
        {
            get => _isValidationVisible;
            set
            {
                SetProperty(ref _isValidationVisible, value);
            }
        }

        private string _validationText;
        public string ValidationText
        {
            get => _validationText;
            set
            {
                SetProperty(ref _validationText, value);
            }
        }

        public AddExpiringThingViewModel()
        {
            Item = new ExpiryngThingDto();
            Item.ExpireDate = DateTime.Now;           
        }

        public void LoadItem(ExpiryngThingDto expiryngThing)
        {
            this.CleanFields();
            this.Item = expiryngThing;            
        }

        public void CleanFields()
        {
            Item.ExpireDate = DateTime.Now;
            this.ValidationText = "";
            this.IsValidationVisible = false;
            this.Item = new ExpiryngThingDto();
        }

        public bool ValidateFields()
        {
            this.ValidationText = "";
            this.IsValidationVisible = false;

            if (string.IsNullOrEmpty(this.Item.Description))
            {
                this.ValidationText = "Description required";
                this.IsValidationVisible = true;
                return false;
            }

            return true;
        }
    }
}
