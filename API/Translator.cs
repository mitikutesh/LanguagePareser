using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace API
{
    public static class Translator
    {
        [FunctionName("TranslatorFunction")]
        [ExponentialBackoffRetry(2, "00:00:04", "00:15:00")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "translations/{lang}/{code}")] HttpRequest req,
            [Blob("resources/{lang}_{lang}.xml", FileAccess.Read), StorageAccount("AzureWebJobsStorage")] Stream stream,
            string lang,
            string code,
            ILogger log,
           [AzureKeyVaultClient] IKeyVaultClient client)
        {
            string responseMessage = String.Empty;

            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                var codeSub = code.Substring("Common_".Length);

                XmlDocument doc = new XmlDocument();
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    doc.Load(reader);
                    //Display all common titles.
                    XmlNodeList elemList = doc.GetElementsByTagName($"{codeSub}");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        responseMessage = elemList[i].InnerXml;
                    }
                }

                responseMessage = string.IsNullOrEmpty(lang)
                    ? "This HTTP triggered function executed successfully. Pass a propery url parameter in the url string."
                    : responseMessage;

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "error");
                responseMessage = ex.Message ?? "This function failed to run";
            }
            return new OkObjectResult(responseMessage);
        }

    }
}
