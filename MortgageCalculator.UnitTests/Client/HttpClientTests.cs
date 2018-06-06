using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MortgageCalculator.UnitTests
{
    [TestFixture]
    public class HttpClientTests
    {
        private HttpClient client;

        private HttpResponseMessage response;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:59608/");
            response = client.GetAsync("api/Mortgage").Result;
        }

       
        [Test(Description ="Check the Http response status code")]
        public void GetHttpResponseStatusTest()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        
        [Test(Description = "Test HTTP response not null")]
        public void CheckResponseNotNullTest()
        {
            Assert.NotNull(response.Content);
        }

        
        [Test(Description = "Test If the response is in Json format")]
        public void IsValidJsonResponseTest()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

        
        [Test(Description = "Test response cache")]
        public void DataCachingTest()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(false, response.Headers.CacheControl.NoCache);
                Assert.AreEqual(1440, response.Headers.CacheControl.MaxAge.Value.TotalMinutes);
            });
        }
    }
}
