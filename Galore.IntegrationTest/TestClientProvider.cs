using System.Net.Http;
using Galore.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Galore.IntegrationTest
{
    public class TestClientProvider
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            var connectionString = "Server=tcp:galore-server.database.windows.net,1433;Initial Catalog=galore;Persist Security Info=False;User ID=galore;Password=G@l0re.is;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>().UseSetting("ConnectionStrings:DefaultConnection", connectionString));
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}