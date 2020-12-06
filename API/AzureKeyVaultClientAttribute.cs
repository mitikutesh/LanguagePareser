using Microsoft.Azure.WebJobs.Description;
using System;

namespace API
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    [Binding]
    public sealed class AzureKeyVaultClientAttribute : Attribute
    {
    }
}
