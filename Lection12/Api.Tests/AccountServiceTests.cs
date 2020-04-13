using Calculator;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void Test_Debit()
        {
            var calculatorMock = new CalculatorMock();
            var service = new AccountService(calculatorMock);

            var accountId = service.CreateAccount();
            
            service.Debit(accountId, 100);

            var balance = service.GetBalance(accountId);
            
            Assert.Equal(-100, balance);
        }
        
        [Theory]
        [InlineData(10, -10)]
        [InlineData(100, -100)]
        [InlineData(1000, -1000)]
        public void Test_Debit_Moq(double amount, double expected)
        {
            var calculatorMock = new Mock<ICalculator>();
            calculatorMock.Setup(c => c.Sum(It.Is<double>(a => a == 0),
                                            It.Is<double>(b => b == -amount)))
                          .Returns(-amount);
            
            var service = new AccountService(calculatorMock.Object);

            var accountId = service.CreateAccount();
            
            service.Debit(accountId, amount);

            var balance = service.GetBalance(accountId);
            
            Assert.Equal(expected, balance);
        }
    }
}
