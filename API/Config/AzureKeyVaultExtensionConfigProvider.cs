using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs.Host.Config;
using System;
using System.Collections.Generic;
using System.Text;
namespace API.Config
{
    internal class AzureKeyVaultExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly IKeyVaultClient _keyVaultClient;

        public AzureKeyVaultExtensionConfigProvider(IKeyVaultClient keyVaultClient) =>
            _keyVaultClient = keyVaultClient;

        public void Initialize(ExtensionConfigContext context) =>
            context
                .AddBindingRule<AzureKeyVaultClientAttribute>()
                .BindToInput(_ => _keyVaultClient);

    }
}
