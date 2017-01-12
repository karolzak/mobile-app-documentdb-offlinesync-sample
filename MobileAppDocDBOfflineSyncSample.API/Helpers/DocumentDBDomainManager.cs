using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;
using System.Web.Http;
using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server.Tables;
using MobileAppDocDBOfflineSyncSample.API.Helpers;

namespace MobileAppDocDBOfflineSyncSampleService.Helpers
{
    public class DocumentDBDomainManager<TDocument, TColorViewModel> : IDomainManager<TColorViewModel> 
        where TDocument : DocumentResource<TColorViewModel>, ITableData
        where TColorViewModel : class, IConvertableViewModel<TDocument>, new()
    {
        public HttpRequestMessage Request { get; set; }
        //public ApiServices Services { get; set; }


        private string _collectionId;
        private string _databaseId;
        private Database _database;
        private DocumentCollection _collection;
        private DocumentClient _client;

        public DocumentDBDomainManager(string collectionId, string databaseId, HttpRequestMessage request)//, ApiServices services)
        {
            Request = request;
            //Services = services;
            _collectionId = collectionId;
            _databaseId = databaseId;
        }

    //    public DocumentDBDomainManager(HttpRequestMessage request, ApiServices services)
    //{
    //    var attribute = typeof(TDocument).GetCustomAttributes(typeof(DocumentAttribute), true).FirstOrDefault() as DocumentAttribute;
    //    if (attribute == null)
    //        throw new ArgumentException("the model class must be decorated with the Document attribute");

    //    Request = request;
    //    Services = services;
    //    _collectionId = attribute.CollectionId;
    //    _databaseId = attribute.DatabaseId;
    //}

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var doc = GetDocument(id);


                if (doc == null)
                {
                    return false;
                }

                await Client.DeleteDocumentAsync(doc.SelfLink);

                return true;


            }
            catch (Exception ex)
            {
                //Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<TColorViewModel> InsertAsync(TColorViewModel data)
        {
            try
            {
                var document = data.ConvertToDocument();

                document.CreatedAt = DateTimeOffset.UtcNow;
                document.UpdatedAt = DateTimeOffset.UtcNow;
                document = await Client.CreateDocumentAsync(Collection.SelfLink, document).ContinueWith<TDocument>(t=> GetDocFromResponse(t));

                var viewModel = new TColorViewModel();
                viewModel.CopyFromDocument(document);
                return viewModel;
            }
            catch (Exception ex)
            {
                //Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        private TDocument GetDocFromResponse(Task<ResourceResponse<Document>> source)
        {
            if (source.IsFaulted)
            {
                new InvalidOperationException("Parent task is faulted.",source.Exception);
            }

            return  GetDocEntity(source.Result.Resource);                
        }

        private TDocument GetDocEntity(Document source)
        {
            if (source == null)
            {
                new ArgumentNullException("source");
            }
                
            return JsonConvert.DeserializeObject<TDocument>(JsonConvert.SerializeObject(source));
               
        }

        public SingleResult<TColorViewModel> Lookup(string id)
        {
            var qry = this.Query().Where(d => d.Id == id);

            var result = qry.ToList<TColorViewModel>();

            return SingleResult.Create<TColorViewModel>(result.AsQueryable());
        }

        public Task<SingleResult<TColorViewModel>> LookupAsync(string id)
        {
            try
            {
                return Task<SingleResult<TColorViewModel>>.Run(()=> Lookup(id));
            }
            catch (Exception ex)
            {
                //Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IQueryable<TColorViewModel> Query()
        {
            try
            {
                var qry = Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink).ToList();

                List<TColorViewModel> viewModels = new List<TColorViewModel>();
                foreach (var doc in qry)
                {
                    var viewModel = new TColorViewModel();
                    viewModel.CopyFromDocument(doc);
                    viewModels.Add(viewModel);
                }

                return viewModels.AsQueryable();
            }
            catch (Exception ex)
            {
                //Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public Task<IEnumerable<TColorViewModel>> QueryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TColorViewModel> UpdateAsync(string id, Delta<TColorViewModel> patch)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (patch == null)
            {
                throw new ArgumentNullException("patch");
            }


            var doc = this.GetDocument(id);
            if (doc  == null)
            {
                //Services.Log.Error(string.Format( "Resource with id {0} not found", id));
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }


            TDocument current =  this.GetDocEntity(doc);
            TColorViewModel currentViewModel = new TColorViewModel();
            currentViewModel.CopyFromDocument(current);
            patch.Patch(currentViewModel);
            this.VerifyUpdatedKey(id, currentViewModel);
            current.UpdatedAt = DateTimeOffset.UtcNow;

            try
            {
                var updatedDoc = await Client.ReplaceDocumentAsync(current.SelfLink, current).ContinueWith<TDocument>(t => GetDocFromResponse(t));
                TColorViewModel updatedViewModel = new TColorViewModel();
                updatedViewModel.CopyFromDocument(updatedDoc);
                return updatedViewModel;
            }

            catch (Exception ex)
            {
                //Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<TColorViewModel> ReplaceAsync(string id, TColorViewModel data)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.VerifyUpdatedKey(id, data);
            data.CreatedAt = DateTimeOffset.UtcNow;

            var doc = this.GetDocument(id);

            try
            {
                var replacedDoc = await Client.ReplaceDocumentAsync(doc.SelfLink, data.ConvertToDocument()).ContinueWith<TDocument>(t => GetDocFromResponse(t));
                TColorViewModel replacedViewModel = new TColorViewModel();
                replacedViewModel.CopyFromDocument(replacedDoc);
                return replacedViewModel;
            }

            catch (Exception ex)
            {
                //Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }

        }

        private TDocument GetDocument(string id)
        {
            return Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink)
                        .Where(d => d.Id == id)
                        .AsEnumerable()
                        .FirstOrDefault();
        }
        private void VerifyUpdatedKey(string id, TColorViewModel data)
        {
            if (data == null || data.Id != id)
            {
                HttpResponseMessage badKey = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The keys don't match");
                throw new HttpResponseException(badKey);
            }
        }

        #region DocumentDBClient

        private DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    _client = new DocumentClient(endpointUri, authKey);
                }

                return _client;
            }
        }

        private DocumentCollection Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return _collection;
            }
        }

        private Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = ReadOrCreateDatabase();
                }

                return _database;
            }
        }

        private DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                                .Where(c => c.Id == _collectionId)
                                .AsEnumerable()
                                .FirstOrDefault();

            if (col == null)
            {
                col = Client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = _collectionId }).Result;
            }

            return col;
        }

        private Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == _databaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = _databaseId }).Result;
            }

            return db;
        }
        #endregion

        public Task<IEnumerable<TColorViewModel>> QueryAsync(System.Web.Http.OData.Query.ODataQueryOptions query)
        {
            throw new NotImplementedException();
        }
    }
}
