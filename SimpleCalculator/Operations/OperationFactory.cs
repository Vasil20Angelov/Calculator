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
                { Division.Symbol, new Division() },
                { Exponentiation.Symbol, new Exponentiation() }
            };
        }
        public IOperation Create(char symbol)
        {
            if (!OperationExists(symbol))
            {
                throw new UndefinedOperationException($"\'{symbol}\' is not a valid operation!");
            }

            return operations[symbol];         
        }

        public bool OperationExists(char symbol)
        {
            return operations.ContainsKey(symbol);
        }
    }
}
