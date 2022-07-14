namespace Calculator.Operations
{
    public class Addition : IOperation
    {
        public const char Symbol = '+';
        public Priority Priority => Priority.Low;
        public double Apply(double num1, double num2)
        {
            return num1 + num2;
        }
    }
}
