using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Galore.Models.Review;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Galore.IntegrationTest
{
    [TestClass]
    public class ReviewIntegrationTest
    {
        [TestMethod]
        public async Task CreateUserReviewForTape()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/users/100/reviews/10", new StringContent(
                    JsonConvert.SerializeObject(new ReviewInputModel()
                    {
                        Score = 4,
                    }),
                    Encoding.UTF8, "application/json"
                ));
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Assert.IsNotNull(response.Headers.Location);
            }
        }

        [TestMethod]
        public async Task GetAllReviewsForUser()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users/100/reviews");
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                IEnumerable<ReviewDTO> reviews = JsonConvert.DeserializeObject<IEnumerable<ReviewDTO>>(data);
                Assert.IsNotNull(reviews);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        } 

        [TestMethod]
        public async Task GetUserReviewForTape()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users/100/reviews/10");
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                ReviewDTO review = JsonConvert.DeserializeObject<ReviewDTO>(data);
                Assert.IsNotNull(review);
                Assert.AreEqual(review.UserId, 100);
                Assert.AreEqual(review.TapeId, 10);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }


        [TestMethod]
        public async Task UpdateUserReviewForTape()
        {
            using(var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/users/100/reviews/10");
                data.EnsureSuccessStatusCode();
                string json = await data.Content.ReadAsStringAsync();
                ReviewDTO review = JsonConvert.DeserializeObject<ReviewDTO>(json);

                var response = await client.PutAsync("/v1/api/users/100/reviews/10", new StringContent(
                    JsonConvert.SerializeObject(new ReviewInputModel()
                    {
                        Score = 3
                    }),
                    Encoding.UTF8, "application/json"
                ));

                var updatedData = await client.GetAsync("/v1/api/users/100/reviews/10");
                updatedData.EnsureSuccessStatusCode();
                string updatedJson = await updatedData.Content.ReadAsStringAsync();
                ReviewDTO updatedReview = JsonConvert.DeserializeObject<ReviewDTO>(updatedJson);
                Assert.AreNotEqual(review.Score, updatedReview.Score);
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            }
        }

        [TestMethod]
        public async Task DeleteUserReviewForTape()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/v1/api/users/100/reviews/10");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }


        [TestMethod]
        public async Task CreateUserReviewForTape2()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/users/100/reviews/10", new StringContent(
                    JsonConvert.SerializeObject(new ReviewInputModel()
                    {
                        Score = 4,
                    }),
                    Encoding.UTF8, "application/json"
                ));
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Assert.IsNotNull(response.Headers.Location);
            }
        }

        [TestMethod]
        public async Task GetAllReviewsForAllTapes()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/tapes/reviews");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task GetUserReviewForTape2()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/tapes/10/reviews/100");
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                ReviewDTO review = JsonConvert.DeserializeObject<ReviewDTO>(data);
                Assert.IsNotNull(review);
                Assert.AreEqual(review.UserId, 100);
                Assert.AreEqual(review.TapeId, 10);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task UpdateUserReviewForTape2()
        {
            using(var client = new TestClientProvider().Client)
            {
                var data = await client.GetAsync("/v1/api/tapes/10/reviews/100");
                data.EnsureSuccessStatusCode();
                string json = await data.Content.ReadAsStringAsync();
                ReviewDTO review = JsonConvert.DeserializeObject<ReviewDTO>(json);

                var response = await client.PutAsync("/v1/api/tapes/10/reviews/100", new StringContent(
                    JsonConvert.SerializeObject(new ReviewInputModel()
                    {
                        Score = 3
                    }),
                    Encoding.UTF8, "application/json"
                ));

                var updatedData = await client.GetAsync("/v1/api/tapes/10/reviews/100");
                updatedData.EnsureSuccessStatusCode();
                string updatedJson = await updatedData.Content.ReadAsStringAsync();
                ReviewDTO updatedReview = JsonConvert.DeserializeObject<ReviewDTO>(updatedJson);
                Assert.AreNotEqual(review.Score, updatedReview.Score);
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            }
        }

        [TestMethod]
        public async Task DeleteUserReviewForTape2()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/v1/api/tapes/10/reviews/100");
                response.EnsureSuccessStatusCode();
            }
        }

    }
}