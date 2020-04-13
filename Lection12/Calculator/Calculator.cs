namespace Calculator
{
    public class Calculator : ICalculator
    {
        public double Sum(string a, string b)
        {
            var aD = double.Parse(a);
            var bD = double.Parse(b);

            return aD + bD;
        }

        public double Sum(double a, double b)
        {
            return a + b;
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