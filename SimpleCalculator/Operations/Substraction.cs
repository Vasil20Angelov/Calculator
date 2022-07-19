namespace Calculator.Operations
{
    public class Substraction : IOperation
    {
        public const char Symbol = '-';
        public char Type => Symbol;
        public Priority Priority => Priority.Low;
        public double Apply(double num1, double num2)
        {
            return num1 - num2;
        }
    }
}
