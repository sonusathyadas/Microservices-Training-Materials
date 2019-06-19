using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using SQLAPIDemo.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAPIDemo
{
    class Program
    {
        private static DocumentClient client;

        //Assign a name for your database & collection 
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string collectionId = ConfigurationManager.AppSettings["CollectionId"];     
        private static readonly string endpointUrl = ConfigurationManager.AppSettings["EndPointUrl"];
        private static readonly string authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string [] args)
        {
            try
            {
                //Get a Document client
                using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
                {
                    Database database = client.CreateDatabaseQuery()
                        .Where(db => db.Id == databaseId)
                        .ToArray()
                        .FirstOrDefault();
                    if (database == null)
                    {
                        database = await client.CreateDatabaseAsync(new Database { Id = databaseId });
                    }

                    //Get, or Create, the Document Collection
                    DocumentCollection collection = client.CreateDocumentCollectionQuery(database.SelfLink)
                        .Where(c => c.Id == collectionId)
                        .ToArray()
                        .FirstOrDefault();
                    if (collection == null)
                    {
                        collection = await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection { Id = collectionId });
                    }

                    //Add document
                    var customer= new Customer()
                    {
                        FirstName="Michel",
                        LastName="Angelo",
                        Address="A434, CA, USA",
                        Orders=new []
                        {
                              new Order
                              {
                                  Id=10203,
                                  OrderDate=DateTime.Now,
                                  Items=new[]
                                  {
                                      new Item{ Id=2, Name="Mango", Price=44, Quantity=6}, 
                                      new Item{ Id=6, Name="Orange", Price=54, Quantity=5}
                                  }
                              }
                        }
                          
                    };
                    //await client.CreateDocumentAsync(collection.SelfLink, customer);
                    //Console.WriteLine("Customer details added");
                    //Read document
                    var document=client.CreateDocumentQuery<Customer>(collection.SelfLink)
                        .Where(doc => doc.FirstName == "Michel")
                        .ToArray()
                        .FirstOrDefault();
                    Console.WriteLine($"Lastname :{document.LastName}");
                    
                }
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
            
        }
    }
}
