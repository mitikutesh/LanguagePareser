using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Test.Helper
{
    public abstract class FunctionTest
    {
        protected readonly ILogger log = NullLoggerFactory.Instance.CreateLogger("Test");

        public DefaultHttpRequest GenerateHttpRequest()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            var queryParams = new Dictionary<String, StringValues>() { };
            request.Query = new QueryCollection(queryParams);

            return request;
        }

        public Stream CreateStream(string xmlName)
        {
            string xmlpath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Files\\{xmlName}");

            string xmlData = File.ReadAllText(xmlpath);

            byte[] dataBuffer = Encoding.UTF8.GetBytes(xmlData);

            var stream = new MemoryStream(dataBuffer);
            return stream;
        }


    }

}
