using SmartApp.Common.DTO;
using SmartApp.Common.Interfaces.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartApp.App.Models;

namespace SmartApp.App.ViewModels.ExpiringThings
{
    public class ExpiringThingsViewModel : BaseViewModel
    {
        private readonly IExpiryngThingClient _expiryngThingClient;
        private ExpiringThingsModel _selectedItem;
        private PagedResult<ExpiryngThingDto> _clientPageInfo;
        private int pageNo = 0;
        private int pageSize = 50;
        private int _itemTreshold;        
        public event EventHandler<ExpiringThingsModel> ScrollToEvent;
        private bool _loadingMoreItems = false;
        private bool _displayPopup;
        public bool DisplayPopup
        {
            get => _displayPopup;
            set
            {
                SetProperty(ref _displayPopup, value);
                
            }
        }

        public ExpiringThingsModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        public ObservableCollection<ExpiringThingsModel> Items { get; }
        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }
        
        public Command ItemsThresholdReachedCommand { get; }


        public ExpiringThingsViewModel(IExpiryngThingClient expiryngThingClient)
        {
            _expiryngThingClient = expiryngThingClient;
            Items = new ObservableCollection<ExpiringThingsModel>();
            LoadItemsCommand = new Command(async () => {                
                await ExecuteLoadItemsCommand();
                this.IsBusy = false;
            });

            AddItemCommand = new Command(() => { this.DisplayPopup = true; });

            ItemsThresholdReachedCommand = new Command(async () => await LoadNextItems());
        }

        async Task LoadNextItems()
        {
            if (IsBusy)
                return;
            
            IsBusy = true;

            try
            {               
                this.pageNo++;
                var previousLastItem = Items.Last();
                _clientPageInfo = await _expiryngThingClient.GetAllExpiryngThings(pageNo, pageSize);
                foreach (var item in _clientPageInfo.Data)
                {
                    Items.Add(new ExpiringThingsModel { Data = item });
                }
                this.ScrollToEvent.Invoke(this, previousLastItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Items.Clear();
                _clientPageInfo = await _expiryngThingClient.GetAllExpiryngThings(pageNo, pageSize);
                foreach (var item in _clientPageInfo.Data)
                {
                    Items.Add(new ExpiringThingsModel { Data= item });
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnAppearing()
        {

            pageNo = 0;
            this._clientPageInfo = null;
            SelectedItem = null;
            ExecuteLoadItemsCommand();
        }

        async Task OnItemSelected(ExpiringThingsModel item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

    }
}
