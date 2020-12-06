using API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TranslateorTest : FunctionTest
    {
         [TestMethod]
        public async Task Request_With_Parameter()
        {
           
            var result = await Translator.Run(
                    req: GenerateHttpRequest(), 
                    stream: CreateStream("en_en.xml"),
                    lang:"en",
                    code: "Common_CancelButtonText",
                    log: log,
                    client: null);
            var resultObject = (OkObjectResult)result;
            Assert.IsInstanceOfType(resultObject, typeof(OkObjectResult));
            Assert.AreEqual("Cancel", resultObject.Value);

        }

        [TestMethod]
        public async Task Request_With_WrongParamter()
        {
            var result = await Translator.Run(
                      req: GenerateHttpRequest(),
                    stream: CreateStream("fi_fi.xml"),
                    lang: "fi",
                    code: "Common_OKButton",
                    log: log,
                    client: null);
            var resultObject = (OkObjectResult)result;
            Assert.AreNotEqual("", resultObject.Value);

        }

    }
}
