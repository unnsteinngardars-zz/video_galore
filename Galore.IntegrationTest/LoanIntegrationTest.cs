using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Galore.IntegrationTest
{
    [TestClass]
    public class LoanIntegrationTest
    {

        [TestMethod]
        public async Task RegisterTapeOnLoan()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/users/100/tapes/10", null);
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task GetTapesOnLoanForUser()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users/100/tapes");
                response.EnsureSuccessStatusCode();
                
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<TapeDTO> tapes = JsonConvert.DeserializeObject<IEnumerable<TapeDTO>>(json);
                var tape = tapes.FirstOrDefault(t => t.Id == 10);
                Assert.IsNotNull(tape);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task UpdateBorrowingInformation()
        {
            using(var client = new TestClientProvider().Client)
            {
              
                var response = await client.PutAsync("/v1/api/users/100/tapes/10", new StringContent(
                    JsonConvert.SerializeObject(new LoanInputModel()
                    {
                        BorrowDate = "2018-10-23"
                    }),
                    Encoding.UTF8, "application/json"
                ));

                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
           
            }
        }

        [TestMethod]
        public async Task ReturnLoanedTape()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/v1/api/users/100/tapes/10");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Invalid User ID")]
        public async Task RegisterTapeOnLoanInvalidUserId()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/v1/api/users/99999/tapes/10", null);
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Invalid User ID")]
        public async Task GetTapesOnLoanForUserInvalidUserId()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/v1/api/users/99999/tapes");
                response.EnsureSuccessStatusCode();
                
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<TapeDTO> tapes = JsonConvert.DeserializeObject<IEnumerable<TapeDTO>>(json);
                var tape = tapes.FirstOrDefault(t => t.Id == 10);
                Assert.IsNotNull(tape);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Loan does not exists")]
        public async Task UpdateBorrowingInformationLoanNotExisting()
        {
            using(var client = new TestClientProvider().Client)
            {
              
                var response = await client.PutAsync("/v1/api/users/1/tapes/1", new StringContent(
                    JsonConvert.SerializeObject(new LoanInputModel()
                    {
                        BorrowDate = "2018-10-23"
                    }),
                    Encoding.UTF8, "application/json"
                ));

                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
           
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "Loan does not exists")]
        public async Task ReturnLoanedTapeLoanNotExisting()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/v1/api/users/1/tapes/1");
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}