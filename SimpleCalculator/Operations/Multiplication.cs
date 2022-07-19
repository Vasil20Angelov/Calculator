namespace Calculator.Operations
{
    public class Multiplication : IOperation
    {
        public const char Symbol = '*';
        public char Type => Symbol;
        public Priority Priority => Priority.High;
        public double Apply(double num1, double num2)
        {
            return num1 * num2;
        }
    }
}
