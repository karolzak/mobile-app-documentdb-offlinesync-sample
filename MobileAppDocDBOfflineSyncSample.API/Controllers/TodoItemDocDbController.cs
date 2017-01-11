using Microsoft.Azure.Mobile.Server;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using MobileAppDocDBOfflineSyncSampleService.DataObjects;
using MobileAppDocDBOfflineSyncSampleService.Helpers;

namespace MobileAppDocDBOfflineSyncSampleService.Controllers
{
    public class TodoItemDocDbController : TableController<TodoItemDocDb>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            string collectioName = ConfigurationManager.AppSettings["collectionName"];
            string databaseId = ConfigurationManager.AppSettings["databaseId"];

            //Using Custom Domain Manager
            DomainManager = new DocumentDBDomainManager<TodoItemDocDb>(collectioName, databaseId, Request);//, Services);
        }


        public IQueryable<TodoItemDocDb> GetAllTodoItems()
        {
            return Query();
        }

        public SingleResult<TodoItemDocDb> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        public Task<TodoItemDocDb> PatchTodoItem(string id, Delta<TodoItemDocDb> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostTodoItem(TodoItemDocDb item)
        {
            TodoItemDocDb current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}