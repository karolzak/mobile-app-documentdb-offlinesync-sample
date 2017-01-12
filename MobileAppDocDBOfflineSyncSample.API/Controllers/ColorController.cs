using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Mobile.Server;
using MobileAppDocDBOfflineSyncSample.API.TableViewModels;
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
    public class ColorController : TableController<ColorViewModel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            string collectioName = ConfigurationManager.AppSettings["colorCollectionName"];
            string databaseId = ConfigurationManager.AppSettings["databaseId"];

            //Using Custom Domain Manager
            DomainManager = new DocumentDBDomainManager<Color, ColorViewModel>(collectioName, databaseId, Request);
        }


        public IQueryable<ColorViewModel> GetAllColors()
        {
            return Query();
        }

        public SingleResult<ColorViewModel> GetColor(string id)
        {
            return Lookup(id);
        }

        public Task<ColorViewModel> PatchColor(string id, Delta<ColorViewModel> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostColor(ColorViewModel item)
        {
            ColorViewModel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteColor(string id)
        {
            return DeleteAsync(id);
        }
    }
}
