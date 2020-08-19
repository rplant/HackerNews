using HackNews.Business;
using HackNews.Interfaces;
using HackNews.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace HackNews.Tests
{

    [TestClass]
    public class HackNewsBusinessTests
    {
        private Mock<IMemoryCache> _memoryCache;
        private HackNewsRepository _hackNewsRepository;

        [TestInitialize]
        public void Setup()
        {
            _memoryCache = new Mock<IMemoryCache>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[ 9129911, 9129199, 9127761, 9128141, 9128264, 9127792, 9129248, 9127092, 9128367, 9038733]"),
               })
               .Verifiable();

            _hackNewsRepository = new HackNewsRepository();
            _hackNewsRepository.client = new HttpClient(handlerMock.Object);  

        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task Get_IEnumerable_Of_HackStories()
        {
            var repo = new HackNewsRepository();
            var business = new HackNewsBusiness(_memoryCache.Object, _hackNewsRepository);
            var result = await business.GetNewHackStories("");

            Assert.AreEqual(result.Count, 0);
        }


    }
}
