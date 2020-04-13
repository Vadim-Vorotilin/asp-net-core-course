using Calculator;

namespace Api.Tests
{
    public class CalculatorMock : ICalculator
    {
        public double Sum(string a, string b)
        {
            throw new System.NotImplementedException();
        }

        public double Sum(double a, double b)
        {
            if (a == 0 && b == -100)
                return -100;
            
            throw new System.NotImplementedException();
        }

        public double Pow(double a, int p)
        {
            throw new System.NotImplementedException();
        }

        public double CircleArea(double radius)
        {
            throw new System.NotImplementedException();
        }
    }
}