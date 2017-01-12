/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=717898
 */
#define OFFLINE_SYNC_ENABLED

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync
using MobileAppDocDBOfflineSyncSample.Shared.DataModel;

namespace MobileAppDocDBOfflineSyncSample.iOS
{
    public class QSTodoService
    {
        static QSTodoService instance = new QSTodoService ();

        const string applicationURL = @"https://XamarinMobileService.azurewebsites.net";

        private MobileServiceClient client;
        const string localDbPath    = "localstore.db";

        private IMobileServiceSyncTable<ToDoItemDocDb> todoTable;

        private QSTodoService ()
        {
            CurrentPlatform.Init();

            // Initialize the client with the mobile app backend URL.
            client = new MobileServiceClient(applicationURL);
            
            // Initialize the store
            InitializeStoreAsync().Wait();

            // Create an MSTable instance to allow us to work with the TodoItem table
            todoTable = client.GetSyncTable<ToDoItemDocDb>();
        }

        public static QSTodoService DefaultService {
            get {
                return instance;
            }
        }

        public List<ToDoItemDocDb> Items { get; private set;}

        public async Task InitializeStoreAsync()
        {
            var store = new MobileServiceSQLiteStore(localDbPath);
            store.DefineTable<ToDoItemDocDb>();

            // Uses the default conflict handler, which fails on conflict
            // To use a different conflict handler, pass a parameter to InitializeAsync.
			// For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
            await client.SyncContext.InitializeAsync(store);

        }

        public async Task SyncAsync(bool pullData = false)
        {
            try
            {
                await client.SyncContext.PushAsync();

                if (pullData) {
                    await todoTable.PullAsync("allTodoItems", todoTable.CreateQuery()); // query ID is used for incremental sync
                }
            }

            catch (MobileServiceInvalidOperationException e)
            {
                Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }

        public async Task<List<ToDoItemDocDb>> RefreshDataAsync ()
        {
            try {
                // Update the local store
                await SyncAsync(pullData: true);

                // This code refreshes the entries in the list view by querying the local TodoItems table.
                // The query excludes completed TodoItems
                Items = await todoTable
                        .Where (todoItem => todoItem.Complete == false).ToListAsync ();

            } catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
                return null;
            }

            return Items;
        }

        public async Task InsertTodoItemAsync (ToDoItemDocDb todoItem)
        {
            try {
                await todoTable.InsertAsync (todoItem); // Insert a new TodoItem into the local database.
#if OFFLINE_SYNC_ENABLED
                await SyncAsync(); // Send changes to the mobile app backend.
#endif

                Items.Add (todoItem);

            } catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
            }
        }

        public async Task CompleteItemAsync (ToDoItemDocDb item)
        {
            try {
                item.Complete = true;
                await todoTable.UpdateAsync (item); // Update todo item in the local database
                await SyncAsync(); // Send changes to the mobile app backend.


                Items.Remove (item);

            } catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
            }
        }
    }
}

