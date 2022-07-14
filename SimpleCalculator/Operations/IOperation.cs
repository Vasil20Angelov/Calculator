namespace Calculator.Operations
{
    public enum Priority
    {
        Low,
        High
    }

    public interface IOperation
    {
        public Priority Priority { get; }

        public double Apply(double num1, double num2);
    }
}
