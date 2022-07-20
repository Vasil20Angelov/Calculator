using Calculator.ExpressionBuilders;

namespace Calculator.Operations
{
    public enum Priority
    {
        Low,
        High,
        VeryHigh
    }

    public enum Associativity
    {
        Left,
        Right
    }

    public interface IOperation
    {
        public char Type { get; }
        public Priority Priority { get; }
        public Associativity Associativity { get; }

        public double Apply(double num1, double num2);
    }
}
