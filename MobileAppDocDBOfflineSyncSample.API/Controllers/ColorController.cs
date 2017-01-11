using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Mobile.Server;
using MobileAppDocDBOfflineSyncSampleService.Helpers;
using MobileAppDocDBOfflineSyncSampleService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace MobileAppDocDBOfflineSyncSample.API.Controllers
{
    public class ColorController : TableController<Color>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            string collectioName = ConfigurationManager.AppSettings["colorCollectionName"];
            string databaseId = ConfigurationManager.AppSettings["databaseId"];

            //Using Custom Domain Manager
            DomainManager = new DocumentDBDomainManager<Color>(collectioName, databaseId, Request);
        }


        public IQueryable<Color> GetAllColors()
        {
            return Query();
        }

        public SingleResult<Color> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        public Task<Color> PatchTodoItem(string id, Delta<Color> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostTodoItem(Color item)
        {
            Color current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}
