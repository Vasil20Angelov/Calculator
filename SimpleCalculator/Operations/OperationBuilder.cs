using Calculator.Exceptions;
namespace Calculator.Operations
{
    public class OperationFactory
    {
        private readonly Dictionary<char, IOperation> operations;
        public OperationFactory()
        {
            operations = new Dictionary<char, IOperation>()
            {
                { Addition.Symbol, new Addition() },
                { Substraction.Symbol, new Substraction() },
                { Multiplication.Symbol, new Multiplication() },
                { Division.Symbol, new Division() }
            };
        }
        public IOperation Create(char symbol)
        {
            IOperation? operation = operations.GetValueOrDefault(symbol);
            if (operation == null)
            {
                throw new UndefinedOperationException($"\'{symbol}\' is not a valid operation!");
            }

            return operation;         
        }
    }
}
