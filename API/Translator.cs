using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace API
{
    public static class Translator
    {
        [FunctionName("TranslatorFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "translations/{lang}/{code}")] HttpRequest req,
            [Blob("resources/{lang}_{lang}.xml", FileAccess.Read), StorageAccount("AzureWebJobsStorage")] Stream stream,
            string lang,
            string code,
            ILogger log)
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
                    ? ""
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
