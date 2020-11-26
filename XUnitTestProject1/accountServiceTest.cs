using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using accountService;
using accountService.Controllers;
using accountService.Data;
using accountService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task PutAccount_Ok()
        {
            var id = 2;
                    
        var content = new StringContent(JsonSerializer.Serialize(new Account{Id=3, UserName = "faker", UserEmail = "gg@123.com", Password = "123", UserBio = "hiagsfbkjas", UserType = "landlord", UserMobile = 134542 }), Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PutAsync($"/Accounts/{id}", content);

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

        [Fact]
        public async Task DeleteAccount_Ok()
        {
            var id = 2;
            // Act
            var response = await Client.DeleteAsync($"/Accounts/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
