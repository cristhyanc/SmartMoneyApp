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
using Acr.UserDialogs;

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
        private bool _displayPopup;
        private AddExpiringThingViewModel _addExpiringThingViewModel;

        public bool DisplayAddPopup
        {
            get => _displayPopup;
            set
            {
                SetProperty(ref _displayPopup, value);
                if(!value)
                {
                    this.AddExpiringThingViewModel = null;
                }
            }
        }

        public ExpiringThingsModel SelectedItem
        {
            get => null;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public AddExpiringThingViewModel AddExpiringThingViewModel
        {
            get => _addExpiringThingViewModel;
            set
            {
                SetProperty(ref _addExpiringThingViewModel, value);              
            }
        }

        bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }


        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        public ObservableCollection<ExpiringThingsModel> Items { get; }
        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }

        public Command SaveNewItemCommand { get; }
        public Command CloseNewItemCommand { get; }

        public Command ItemsThresholdReachedCommand { get; }

        public Command DeleteItemCommand { get; }

        public Command RenewItemCommand { get; }

        public ExpiringThingsViewModel(IExpiryngThingClient expiryngThingClient)
        {
            _expiryngThingClient = expiryngThingClient;
            Items = new ObservableCollection<ExpiringThingsModel>();
            LoadItemsCommand = new Command(async () =>
            {
                await ExecuteLoadItemsCommand();              
            });

            AddItemCommand = new Command(() =>
            {
                AddExpiringThingViewModel = new AddExpiringThingViewModel();           
                this.DisplayAddPopup = true;
            });
            SaveNewItemCommand = new Command(async () => await SaveNewItem());
            CloseNewItemCommand = new Command(() => this.DisplayAddPopup = false);
            ItemsThresholdReachedCommand = new Command(async () => await LoadNextItems());
            DeleteItemCommand = new Command<ExpiringThingsModel>(async (data) => await Delete(data));
            RenewItemCommand = new Command<ExpiringThingsModel>((data) => StartRenew(data));           
        }

        void StartRenew(ExpiringThingsModel item)
        {
            if (IsBusy)
                return;
            try
            {
                AddExpiringThingViewModel = new AddExpiringThingViewModel();
                item.Data.ExpireDate = DateTime.Now;

                AddExpiringThingViewModel.LoadItem(item.Data);
                this.DisplayAddPopup = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        async Task Delete(ExpiringThingsModel item)
        {
            if (IsBusy)
                return;
            try
            {
                if ((await UserDialogs.Instance.ConfirmAsync("Do you want to delete it?", "Delete Item", "Yes", "No")))
                {
                    this.IsBusy = true;
                    await _expiryngThingClient.DeleteExpiringThings(item.Data.Id);
                    await ExecuteLoadItemsCommand();                     
                    UserDialogs.Instance.Toast("Deleted!!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        async Task SaveNewItem()
        {
            if(!AddExpiringThingViewModel.ValidateFields())
            {
                this.DisplayAddPopup = true;
                return;
            }

            if (IsBusy)
                return;
            try
            {
                this.IsBusy = true;
                ExpiryngThingDto result = null;

                if (AddExpiringThingViewModel.Item.Id == 0)
                {
                    result = await _expiryngThingClient.CreateExpiringThings(AddExpiringThingViewModel.Item);
                }
                else
                {
                    result = await _expiryngThingClient.UpdateExpiringThings(AddExpiringThingViewModel.Item.Id, AddExpiringThingViewModel.Item);
                }

                if(result!=null)
                {
                     await ExecuteLoadItemsCommand();
                  
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;               
            }
            this.DisplayAddPopup = false;
        }

        async Task LoadNextItems()
        {
            if (IsBusy || this.pageSize>this.Items?.Count )
                return;
            
            IsBusy = true;

            try
            {               
                this.pageNo++;
                var previousLastItem = Items.Last();
                _clientPageInfo = await _expiryngThingClient.GetAllExpiringThings(pageNo, pageSize);
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
           
            //this.IsRefreshing = true;

            try
            {
                Items.Clear();
                _clientPageInfo = await _expiryngThingClient.GetAllExpiringThings(pageNo, pageSize);
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
                this.IsRefreshing = false;
            }
        }

        async Task LoadItems()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Items.Clear();
                _clientPageInfo = await _expiryngThingClient.GetAllExpiringThings(pageNo, pageSize);
                foreach (var item in _clientPageInfo.Data)
                {
                    Items.Add(new ExpiringThingsModel { Data = item });
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

        void OnItemSelected(ExpiringThingsModel item)
        {
            if (IsBusy || item == null)
                return;
            try
            {
                this.AddExpiringThingViewModel = new AddExpiringThingViewModel();
                AddExpiringThingViewModel.LoadItem(item.Data);                
                this.DisplayAddPopup = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

    }
}
