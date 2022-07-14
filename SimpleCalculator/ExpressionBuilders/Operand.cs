namespace Calculator.ExpressionBuilders
{
    public class Operand : IExpressionPart
    {
        public double Value { get; }
        public Operand(double value)
        {
            Value = value;
        }

    }
}
