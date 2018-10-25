using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Galore.Models.Exceptions;
using Galore.Models.Tape;
using Galore.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;


namespace Galore.IntegrationTest
{
    [TestClass]
    public class TapeIntegrationTest
    {
        [TestMethod]
        public async Task GetAllTapes()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/tapes");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task GetTapeByValidID()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/tapes/1");
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                TapeDetailDTO tape = JsonConvert.DeserializeObject<TapeDetailDTO>(data);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(1, tape.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Resource not found")]
        public async Task GetTapeByInvalidId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("v1/api/tapes/1111111");
                response.EnsureSuccessStatusCode();
            }
        }

        [TestMethod]
        public async Task CreateTapeValidInput()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/tapes", new StringContent(
                    JsonConvert.SerializeObject(new TapeInputModel()
                    {
                        Title = "Psycho",
                        DirectorFirstName = "Alfred",
                        DirectorLastName = "Hitchcock",
                        Type = "betamax",
                        EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                        ReleaseDate = "2000-01-01",
                    }),
                    Encoding.UTF8, "application/json"
                ));

                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Assert.IsNotNull(response.Headers.Location);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Invalid input")]
        public async Task CreateTapeInvalidInput()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/tapes", new StringContent(
                    JsonConvert.SerializeObject(new TapeInputModel()
                    {
                        Title = "blabla",
                    }),
                    Encoding.UTF8, "application/json"
                ));

                response.EnsureSuccessStatusCode();
            }
        }

        [TestMethod]
        public async Task UpdateTapeValidId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/tapes");
                string json = await data.Content.ReadAsStringAsync();
                IEnumerable<TapeDTO> tapes = JsonConvert.DeserializeObject<IEnumerable<TapeDTO>>(json);
                var maxId = tapes.Max(u => u.Id);
                var tape = tapes.FirstOrDefault(u => u.Id == maxId);

                var response = await client.PutAsync($"/v1/api/tapes/{maxId}", new StringContent(
                    JsonConvert.SerializeObject(new TapeInputModel()
                    {
                        Title = "Psycho Updated",
                        DirectorFirstName = "Alfred",
                        DirectorLastName = "Hitchcock",
                        Type = "betamax",
                        EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                        ReleaseDate = "2000-10-10",
                    }),
                    Encoding.UTF8, "application/json"));

                var updateResponse = await client.GetAsync($"/v1/api/tapes/{maxId}");
                string updatedTapeJson = await updateResponse.Content.ReadAsStringAsync();
                TapeDetailDTO updatedTape = JsonConvert.DeserializeObject<TapeDetailDTO>(updatedTapeJson);
                Assert.AreNotEqual(tape.Title, updatedTape.Title);
            }
        }

        [TestMethod]
        public async Task UpdateTapeInvalidInputData()
        {
            using (var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/tapes");
                string json = await data.Content.ReadAsStringAsync();
                IEnumerable<TapeDTO> tapes = JsonConvert.DeserializeObject<IEnumerable<TapeDTO>>(json);
                var maxId = tapes.Max(u => u.Id);
                var tape = tapes.FirstOrDefault(u => u.Id == maxId);

                var response = await client.PutAsync($"/v1/api/tapes/{maxId}", new StringContent(
                    JsonConvert.SerializeObject(new TapeInputModel()
                    {
                        Title = "LOTR",

                    }),
                    Encoding.UTF8, "application/json"));

                var updateResponse = await client.GetAsync($"/v1/api/tapes/{maxId}");
                string updatedTapeJson = await updateResponse.Content.ReadAsStringAsync();
                TapeDetailDTO updatedTape = JsonConvert.DeserializeObject<TapeDetailDTO>(updatedTapeJson);
                Assert.AreEqual(tape.Title, updatedTape.Title);
            }
        }

        [TestMethod]
        public async Task DeleteTapeValidId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/tapes");
                string json = await data.Content.ReadAsStringAsync();
                IEnumerable<TapeDTO> tapes = JsonConvert.DeserializeObject<IEnumerable<TapeDTO>>(json);
                var maxId = tapes.Max(u => u.Id);
                var response = await client.DeleteAsync($"/v1/api/tapes/{maxId}");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Resource not found")]
        public async Task DeleteTapeInvalidId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/v1/api/tapes/99999");
                response.EnsureSuccessStatusCode();
            }
        }

    }
}