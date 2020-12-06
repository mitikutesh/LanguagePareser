using API;
using API.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: WebJobsStartup(typeof(Startup))]
namespace API
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var tempProvider = builder.Services.BuildServiceProvider();
            var config = tempProvider.GetRequiredService<IConfiguration>();
            builder.AddAzureKeyVault(config["AzureKeyVault_Uri"]);
            //builder.AddAzureKeyVault(config["AzureKeyVault_Uri"], 
            //    config["AzureKeyVault_ClientId"], 
            //    config["AzureKeyVault_ClientSecret"]);

        }
    }
}
