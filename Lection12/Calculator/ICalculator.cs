namespace Calculator
{
    public interface ICalculator
    {
        double Sum(string a, string b);
        double Sum(double a, double b);
        double Pow(double a, int p);
        double CircleArea(double radius);
    }
}