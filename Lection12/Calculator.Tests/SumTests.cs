using System;
using Xunit;

namespace Calculator.Tests
{
    public class SumTests
    {
        private readonly ICalculator _calculator;
        
        public SumTests()
        {
            _calculator = CreateCalculator();
        }
        
        [Fact]
        public void Test_Sum_Returns_Correct_Sum()
        {
            var result = _calculator.Sum(1, 2);
            
            Assert.Equal(3, result);
        }
        
        [Fact]
        public void Test_Sum_PositiveInfinity_Returns_PositiveInfinity()
        {
            var result = _calculator.Sum(1, double.PositiveInfinity);
            
            Assert.Equal(double.PositiveInfinity, result);
        }
        
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(10, 20, 30)]
        [InlineData(-10, 20, 10)]
        [InlineData(-10, -20, -30)]
        public void Test_Sum_Returns_ExpectedResult(double a, double b, double expected)
        {
            var result = _calculator.Sum(a, b);
            
            Assert.True(Math.Abs(result - expected) < 1e-10);
        }
        
        [Theory]
        [InlineData(-10, double.PositiveInfinity, double.PositiveInfinity)]
        [InlineData(double.PositiveInfinity, 10, double.PositiveInfinity)]
        [InlineData(-10, double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.NegativeInfinity, 1e+30, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.NegativeInfinity, double.NaN)]
        public void Test_SumInfinity_Returns_ExpectedResult(double a, double b, double expected)
        {
            var result = _calculator.Sum(a, b);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("1", "2", 3)]
        [InlineData("10", "20", 30)]
        public void Test_SumOfStrings_Returns_ExpectedResult(string a, string b, double expected)
        {
            var result = _calculator.Sum(a, b);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("1a", "2")]
        [InlineData("10", "~20")]
        [InlineData("1 0", "20")]
        [InlineData("1*0", "20")]
        [InlineData("1(", "20")]
        public void Test_SumOfStrings_Throws_FormatException(string a, string b)
        {
            Assert.Throws<FormatException>(() => _calculator.Sum(a, b));
        }

        private static ICalculator CreateCalculator()
        {
            return new Calculator();
        }
    }
}
