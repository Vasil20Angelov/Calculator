namespace Calculator.Operations
{
    public class Division : IOperation
    {
        public const char Symbol = '/';
        public char Type => Symbol;
        public Priority Priority => Priority.High;
        public Associativity Associativity => Associativity.Left;
        public double Apply(double num1, double num2)
        {
            if (num2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero!");
            }
            
            return num1 / num2;
        }
    }
}
