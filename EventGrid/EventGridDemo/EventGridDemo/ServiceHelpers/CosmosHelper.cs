using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EventGridDemo.ServiceHelpers
{
    public static class CosmosHelper
    {
        #region Data memebers
        private static string _endpointUri;
        private static string _key;
        private static DocumentClient documentClient;
        #endregion

        #region Properties
        public static string EndpointUri
        {
            get { return _endpointUri; }
            set
            {
                if (value != _endpointUri)
                {                    
                    _endpointUri = value;
                    InitializeService();
                }
            }
        }

        public static string Key
        {
            get { return _key; }
            set
            {
                if (value != _key)
                {                    
                    _key = value;
                    InitializeService();
                }
            }
        }
        #endregion

        #region Private methods
        private static void InitializeService()
        {
            if (!string.IsNullOrEmpty(_endpointUri) && !string.IsNullOrEmpty(_key))
            {
                documentClient = new DocumentClient(new Uri(_endpointUri), _key);
            }
        }
        #endregion

        #region Public methods
        public static async Task<string> CreateDocumentAsync(string dbName, string collectionName, object document)
        {
            await documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id=dbName });

            await documentClient.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(dbName), 
                new DocumentCollection { Id = collectionName });
            var response=await documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, collectionName), document);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return response.Resource.Id;
            }
            else
            {
                throw new Exception("Failed to created document in DocumentDB");
            }
        }
        #endregion
    }
}
