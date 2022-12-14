namespace Calculator.Operations
{
    public class Addition : IOperation
    {
        public const char Symbol = '+';
        public char Type => Symbol;
        public Priority Priority => Priority.Low;
        public Associativity Associativity => Associativity.Left;

        public double Apply(double num1, double num2)
        {
            return num1 + num2;
        }
    }
}
