using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration
{
    public class AccountControllerTests : IClassFixture<ApiWebFactory>
    {
        private readonly ApiWebFactory _factory;

        public AccountControllerTests(ApiWebFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_Debit()
        {
            // AAA
            
            // Arrange
            var client = _factory.CreateClient();
            var content = new ByteArrayContent(new byte[0]);
            
            // Act
            var result = await client.PostAsync("account/createAccount", content);
            var accountId = await result.Content.ReadAsStringAsync();

            var debitResult = await client.PatchAsync($"account/debit?accountid={accountId}&amount=10", content);
            var balance = await debitResult.Content.ReadAsStringAsync();
            
            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(accountId);
            Assert.True(accountId.Length > 0);
            
            Assert.True(debitResult.IsSuccessStatusCode);
            Assert.NotNull(balance);
            Assert.Equal("10", balance);
        }
    }
}