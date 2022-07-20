using Calculator.Operations;
namespace Calculator.ExpressionBuilders
{
    public class Operator : IExpressionPart
    {
        private readonly IOperation operation;
        public char Type => operation.Type;
        public Priority Priority => operation.Priority;
        public Associativity Associativity => operation.Associativity;

        public Operator(IOperation operation)
        {
            this.operation = operation;
        }

        public Operand Apply(Operand operand1, Operand operand2)
        {
            return new Operand(operation.Apply(operand1.Value, operand2.Value));
        }

    }
}
