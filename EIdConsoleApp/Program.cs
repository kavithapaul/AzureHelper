using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.Identity.Client;



using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace EIdConsoleApp
{
    internal class Program
    {


        // Set the variables
        static string tenantID = "<Add Your value here>";
        static string applicationId = "<Add Your value here>";
        static string authenticationKey = "<Add Your value here>"; //client Secret
        static string subscriptionId = "<Add Your value here>";
        static string resourceGroup = "<Add Your value here>";
        static string dataFactoryName = "<Add Your value here>";
        static string pipelineName = "<Add Your value here>";

        static void Main(string[] args)
        {
            try
            {
                //Run the Pipeline
                RunPipeLie();

                //Azure Search
                if (args.Length != 0)
                {
                    search(args[0].ToString());
                }
                else// hard coded search string for testing
                {
                    search("road");
                }

                Console.WriteLine("Press Enter key to exit application");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Logs.Write(ex.StackTrace);
            }

        }
        static void RunPipeLie()
        {


            try
            {
                Console.WriteLine("Press Enter key to run pipeline");
                Console.ReadLine();

                // Authenticate and create a data factory management client
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(applicationId)
                 .WithAuthority("https://login.microsoftonline.com/" + tenantID)
                 .WithClientSecret(authenticationKey)
                 .WithLegacyCacheCompatibility(false)
                 .WithCacheOptions(CacheOptions.EnableSharedCacheOptions)
                 .Build();

                //var val = await Task.Run(async () => { await app.AcquireTokenForClient(new string[] { "https://management.azure.com//.default" }).ExecuteAsync(); });
                Task<AuthenticationResult> task3 = Task<AuthenticationResult>.Run(() => app.AcquireTokenForClient(new string[] { "https://management.azure.com//.default" }).ExecuteAsync());
                //var val = await Task.Run(() => app.AcquireTokenForClient(new string[] { "https://management.azure.com//.default" }).ExecuteAsync());
                AuthenticationResult result = (AuthenticationResult)task3.Result;//await app.AcquireTokenForClient(new string[] { "https://management.azure.com//.default" }).ExecuteAsync();
                ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
                var client = new DataFactoryManagementClient(cred)
                {
                    SubscriptionId = subscriptionId
                };



                // Create a pipeline run
                Console.WriteLine("Creating pipeline run...");

                CreateRunResponse runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(
                    resourceGroup, dataFactoryName, pipelineName, parameters: null
                ).Result.Body;
                Console.WriteLine("Pipeline run ID: " + runResponse.RunId);


                // Monitor the pipeline run
                Console.WriteLine("Checking pipeline run status...");
                PipelineRun pipelineRun;
                while (true)
                {
                    pipelineRun = client.PipelineRuns.Get(
                        resourceGroup, dataFactoryName, runResponse.RunId);
                    Console.WriteLine("Status: " + pipelineRun.Status);
                    if (pipelineRun.Status == "InProgress" || pipelineRun.Status == "Queued")
                        System.Threading.Thread.Sleep(15000);
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Logs.Write(ex.StackTrace);
            }
        }
        static void search(string searchText)
        {
            try
            {
                Console.WriteLine("Azure Search for the string:" + searchText);
                Console.WriteLine("Start Searching...");
                // Get the service endpoint and API key from the environment
                Uri endpoint = new Uri("https://eidcognitivesearch.search.windows.net");
                string key = "wssdX66nYL79zSn3sAunH7SwldPKf6mMD93GvipefRAzSeBPb4pq";//Environment.GetEnvironmentVariable("wssdX66nYL79zSn3sAunH7SwldPKf6mMD93GvipefRAzSeBPb4pq");
                string indexName = "azuresql-index";

                // Create a client
                Azure.AzureKeyCredential credential = new Azure.AzureKeyCredential(key);
                SearchClient searchClient = new SearchClient(endpoint, indexName, credential);
                
                SearchResults<Product> searchResponse = searchClient.Search<Product>(searchText);
                Console.WriteLine("Search Results:");
                foreach (SearchResult<Product> result1 in searchResponse.GetResults())
                {
                    
                    Product doc = result1.Document;
                    Console.WriteLine($"{doc.ProductId}: {doc.ProductName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message );
                Logs.Write(ex.StackTrace);
            }
        }

    }
}
