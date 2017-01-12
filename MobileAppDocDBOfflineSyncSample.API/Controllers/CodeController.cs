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
    public class CodeController : TableController<CodeViewModel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            string collectioName = ConfigurationManager.AppSettings["colorCollectionName"];
            string databaseId = ConfigurationManager.AppSettings["databaseId"];

            //Using Custom Domain Manager
            DomainManager = new DocumentDBDomainManager<Color, CodeViewModel>(collectioName, databaseId, Request);
        }


        public IQueryable<CodeViewModel> GetAllCodes()
        {
            return Query();
        }

        public SingleResult<CodeViewModel> GetCode(string id)
        {
            return Lookup(id);
        }

        public Task<CodeViewModel> PatchCode(string id, Delta<CodeViewModel> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostCode(CodeViewModel item)
        {
            CodeViewModel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteCode(string id)
        {
            return DeleteAsync(id);
        }
    }
}
