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

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync
using MobileAppDocDBOfflineSyncSample.Shared.DataModel;

namespace MobileAppDocDBOfflineSyncSample
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<ToDoItemDocDb, ToDoItemDocDb> todoItems;
        private MobileServiceCollection<ComplexItem, ComplexItem> complexItems;
        private IMobileServiceSyncTable<ComplexItem> complexTable = App.MobileService.GetSyncTable<ComplexItem>();
        private IMobileServiceSyncTable<ToDoItemDocDb> todoTable = App.MobileService.GetSyncTable<ToDoItemDocDb>(); // offline sync

        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitLocalStoreAsync(); // offline sync
            ButtonRefresh_Click(this, null);
        }

        private async Task InsertTodoItem(ToDoItemDocDb todoItem)
        {
            // This code inserts a new TodoItem into the database. After the operation completes
            // and the mobile app backend has assigned an id, the item is added to the CollectionView.
            await todoTable.InsertAsync(todoItem);
            todoItems.Add(todoItem);
            
            try
            {
                await App.MobileService.SyncContext.PushAsync();
            }
            catch { }
        }

        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems.
                todoItems = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
                    .ToCollectionAsync();

                complexItems = await complexTable
                     
                     .ToCollectionAsync();
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
                ListItems.ItemsSource = todoItems;
                this.ButtonSave.IsEnabled = true;
            }
        }

        private async Task UpdateCheckedTodoItem(ToDoItemDocDb item)
        {
            // This code takes a freshly completed TodoItem and updates the database.
			// After the MobileService client responds, the item is removed from the list.
            await todoTable.UpdateAsync(item);
            todoItems.Remove(item);
            ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);
            
            try
            {
                await App.MobileService.SyncContext.PushAsync();
            }
            catch { }
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ButtonRefresh.IsEnabled = false;
            
            await SyncAsync(); // offline sync
            await RefreshTodoItems();

            ButtonRefresh.IsEnabled = true;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var todoItem = new ToDoItemDocDb { Text = TextInput.Text };
            TextInput.Text = "";
            await InsertTodoItem(todoItem);
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            ToDoItemDocDb item = cb.DataContext as ToDoItemDocDb;
            await UpdateCheckedTodoItem(item);
        }

        private void TextInput_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) {
                ButtonSave.Focus(FocusState.Programmatic);
            }
        }

        #region Offline sync
        private async Task InitLocalStoreAsync()
        {
           if (!App.MobileService.SyncContext.IsInitialized)
           {
               var store = new MobileServiceSQLiteStore("localstore.db");
               store.DefineTable<ToDoItemDocDb>();
                store.DefineTable<ComplexItem>();
                await App.MobileService.SyncContext.InitializeAsync(store);
           }

           await SyncAsync();
        }

        private async Task SyncAsync()
        {
            try
            {
                

                await App.MobileService.SyncContext.PushAsync();
                await todoTable.PullAsync("allTodoItemDocDb", todoTable.CreateQuery());
                await complexTable.PullAsync("allComplexItem", complexTable.CreateQuery());
                //lastUpdateTimeStamp = DateTime.Now;
            }
            catch { }

        }
        #endregion
    }
}
