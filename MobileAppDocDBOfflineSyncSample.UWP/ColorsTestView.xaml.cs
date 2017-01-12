
/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=717898
 */

using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MobileAppDocDBOfflineSyncSample.Shared.DataModels;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MobileAppDocDBOfflineSyncSample.UWP
{
    public sealed partial class ColorsTestView : Page
    {
        //public MobileServiceCollection<Color, Color> colorItems;
        //public MobileServiceCollection<Code, Code> codeItems;

        public List<Color> colorsCollection = new List<Color>();
        public List<Code> codesCollection = new List<Code>();

        private IMobileServiceSyncTable<Color> colorTable = App.MobileService.GetSyncTable<Color>(); // offline sync
        private IMobileServiceSyncTable<Code> codeTable = App.MobileService.GetSyncTable<Code>(); // offline sync


        public ColorsTestView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitLocalStoreAsync(); // offline sync
        }

        
        private async Task RefreshColorItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems.
                //colorItems = await colorTable.ToCollectionAsync();
                //codeItems = await codeTable.ToCollectionAsync();
                colorsCollection =await colorTable.ToListAsync();
                codesCollection = await codeTable.ToListAsync();
                
                foreach (var code in codesCollection)
                {
                    try
                    {
                        colorsCollection.Find(q => q.Id == code.Id).Code = code;
                    }
                    catch { }
                }

            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }
            catch (Exception e)
            {

            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                colorsListView.ItemsSource = colorsCollection;
                //todo combine colors with codes
                this.refreshButton.IsEnabled = true;
            }
        }

        

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshButton.IsEnabled = false;
            
            await SyncAsync(); // offline sync
            await RefreshColorItems();

            refreshButton.IsEnabled = true;
        }

        

        #region Offline sync
        private async Task InitLocalStoreAsync()
        {
            if (!App.MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("localstore.db");
                store.DefineTable<Color>();
                store.DefineTable<Code>();
                await App.MobileService.SyncContext.InitializeAsync(store);
            }

            await SyncAsync();
        }

        private async Task SyncAsync()
        {
            try
            {
                await App.MobileService.SyncContext.PushAsync();
                await colorTable.PullAsync("allColor", colorTable.CreateQuery());
                await codeTable.PullAsync("allCode", codeTable.CreateQuery());
            }
            catch { }

        }
        #endregion
    }
}
