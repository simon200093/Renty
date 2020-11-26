using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using accountService;
using accountService.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject1
{
    public class accountServiceTest
    {
        public HttpClient Client { get; }
        public accountServiceTest(ITestOutputHelper outputHelper)
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
               .UseStartup<Startup>());
            Client = server.CreateClient();}

        [Fact]
        public async Task GetAccountById_Ok()
        {
            int id = 1;

            // Act
            var response = await Client.GetAsync($"/Accounts/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAccountByUsername_Ok()
        {
            var user = "test";
            // Act
            var response = await Client.GetAsync($"/Accounts/find/{user}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PostAccount_Ok()
        {
            var content = new StringContent(JsonSerializer.Serialize(new Account { UserName = "test", UserEmail = "gg@123.com", Password = "123", UserBio = "hiagsfbkjas", UserType = "landlord", UserMobile = 134542 }), Encoding.UTF8, "application/json");
            // Act
            var response = await Client.PostAsync($"/Accounts/register", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
