using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TestFixture : IDisposable  
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<AspNetCoreTodo.Startup>()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(Path.Combine(
                        Directory.GetCurrentDirectory(), "..\\..\\..\\..\\AspNetCoreTodo"));

                    configBuilder.AddJsonFile("appsettings.json");
                });
            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}


//Issues
//integration test ==> having issues