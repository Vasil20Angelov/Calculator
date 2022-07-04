using Calculator.Exceptions;

namespace Calculator
{
    public class TextToExpressionParser
    {
        private readonly UnitExtracter extracter;
        public TextToExpressionParser(UnitExtracter unitExtracter) { extracter = unitExtracter; }

        public Expression Parse(string? input)
        {
            if (input == null) throw new NoExpressionException();

            return Split(input.Trim());
        }
        private Expression Split(string input)
        {
            int pos = 0;

            int number1 = extracter.ExtractNumberAt(input, pos++);
            SkipWhiteSpace(input, ref pos);

            Operation operation = extracter.ExtractOperationAt(input, pos++);

            SkipWhiteSpace(input, ref pos);

            int number2 = extracter.ExtractNumberAt(input, pos++);

            AssertEndOfTheStringIsReached(pos, input.Length);

            return new Expression(number1, operation, number2);
        }

        private void SkipWhiteSpace(string input, ref int pos)
        {
            while (pos < input.Length && input[pos] == ' ')
                ++pos;
        }

        private void AssertEndOfTheStringIsReached(int pos, int strLength)
        {
            if (pos < strLength)
                throw new WrongInputException("Invalid input!");
        }
    }
}
