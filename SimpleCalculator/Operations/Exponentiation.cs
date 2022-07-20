namespace Calculator.Operations
{
    public class Exponentiation : IOperation
    {
        public const char Symbol = '^';
        public char Type => Symbol;

        public Priority Priority => Priority.VeryHigh;
        public Associativity Associativity => Associativity.Right;

        public double Apply(double num1, double num2)
        {
            return Math.Pow(num1, num2);
        }
    }
}
