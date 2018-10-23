using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Galore.Models.Exceptions;
using Galore.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Galore.IntegrationTest
{
    [TestClass]
    public class UserIntegrationTest
    {
        [TestMethod]
        public async Task GetAllUsers()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task GetUserByValidID()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users/1");
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                UserDetailDTO user = JsonConvert.DeserializeObject<UserDetailDTO>(data);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(1, user.Id);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Resource not found")]
        public async Task GetUserByInvalidId()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users/111111");
                response.EnsureSuccessStatusCode();
            }
        }

        [TestMethod]
        public async Task CreateUserValidInput()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/users", new StringContent(
                    JsonConvert.SerializeObject(new UserInputModel()
                    {
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Email = "test@test.com",
                        Phone = "1234567",
                        Address = "testgata 22"
                    }),
                    Encoding.UTF8, "application/json"));
                // response.EnsureSuccessStatusCode();
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Assert.IsNotNull(response.Headers.Location);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Invalid input")]
        public async Task CreateUserInvalidInput()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/users", new StringContent(
                    JsonConvert.SerializeObject(new UserInputModel()
                    {
                        FirstName = "blabla"
                    }),
                    Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }   

        [TestMethod]
        public async Task UpdateUserValidId()
        {
            using(var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/users");
                string json = await data.Content.ReadAsStringAsync();
                IEnumerable<UserDTO> users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(json);
                var maxId = users.Max(u => u.Id);
                var user = users.FirstOrDefault(u => u.Id == maxId);

                var response = await client.PutAsync($"/v1/api/users/{maxId}", new StringContent(
                    JsonConvert.SerializeObject(new UserInputModel()
                    {
                        FirstName = "Aragorn",
                        LastName = "Legolasson",
                        Email = "aragorn@legolas.mo",
                        Phone = "6969691",
                        Address = "Isengard 20"
                    }),
                    Encoding.UTF8, "application/json"));

                var updateResponse = await client.GetAsync($"/v1/api/users/{maxId}");
                string updatedUserJson = await updateResponse.Content.ReadAsStringAsync();
                UserDetailDTO updatedUser = JsonConvert.DeserializeObject<UserDetailDTO>(updatedUserJson);
                Assert.AreNotEqual(user.FirstName, updatedUser.FirstName);
            }
        }

        [TestMethod]
        public async Task UpdateUserInvalidInputData()
        {
            using(var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/users");
                string json = await data.Content.ReadAsStringAsync();
                IEnumerable<UserDTO> users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(json);
                var maxId = users.Max(u => u.Id);
                var user = users.FirstOrDefault(u => u.Id == maxId);
                var response = await client.PutAsync($"/v1/api/users/{maxId}", new StringContent(
                    JsonConvert.SerializeObject(new UserInputModel()
                    {
                        FirstName = "Gandalf"
                    }),
                    Encoding.UTF8, "application/json"));

                var updateResponse = await client.GetAsync($"/v1/api/users/{maxId}");
                string updatedUserJson = await updateResponse.Content.ReadAsStringAsync();
                UserDetailDTO updatedUser = JsonConvert.DeserializeObject<UserDetailDTO>(updatedUserJson);
                Assert.AreEqual(user.FirstName, updatedUser.FirstName);
            }    
        }

        [TestMethod]
        public async Task DeleteUserValidId()
        {
            using(var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/users");
                string json = await data.Content.ReadAsStringAsync();
                IEnumerable<UserDTO> users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(json);
                var maxId = users.Max(u => u.Id);
                var response = await client.DeleteAsync($"/v1/api/users/{maxId}");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Resource not found")]
        public async Task DeleteUserInvalidId()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/v1/api/users/1337");
                response.EnsureSuccessStatusCode();
            }
        }


    }
}