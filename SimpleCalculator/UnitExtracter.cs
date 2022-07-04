using Calculator.Exceptions;

namespace Calculator
{
    public class UnitExtracter
    {
        public virtual int ExtractNumberAt(string input, int pos)
        {
            if (pos >= input.Length || !IsNumber(input[pos]))
                throw new WrongInputException("Invalid input!");

            return ConvertCharToInt(input[pos]);
        }

        public virtual Operation ExtractOperationAt(string input, int pos)
        {
            if (pos >= input.Length)
                throw new WrongInputException("Invalid input!");

            Operation operation = (Operation)input[pos];
            if (!IsValidOperation(operation))
                throw new UndefinedOperationException($"\'{input[pos]}\' is not a valid operation!");

            return operation;
        }

        private int ConvertCharToInt(char c) => c - '0';
        private bool IsNumber(char c) => c >= '0' && c <= '9';
        private bool IsValidOperation(Operation operation)
        {
            foreach (Operation op in Enum.GetValues(typeof(Operation)))
                if (operation == op)
                    return true;

            return false;
        }
    }
}
