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
    public class ComplexItemController : TableController<ComplexItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            string collectioName = ConfigurationManager.AppSettings["complexItemCollection"];
            string databaseId = ConfigurationManager.AppSettings["databaseId"];

            //Using Custom Domain Manager
            DomainManager = new DocumentDBDomainManager<ComplexItem>(collectioName, databaseId, Request);//, Services);
        }


        [QueryableExpand("Nested")]
        [QueryableExpand("NestedItems")]

        public IQueryable<ComplexItem> GetAllTodoItems()
        {
            var q = Query();
            return q;
        }

        public SingleResult<ComplexItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        public Task<ComplexItem> PatchTodoItem(string id, Delta<ComplexItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostTodoItem(ComplexItem item)
        {
            ComplexItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}