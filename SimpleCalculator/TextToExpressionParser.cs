using Calculator.Exceptions;
using Calculator.ExpressionBuilders;
using Calculator.Operations;

namespace Parser
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

            List<IExpressionPart> expr = new List<IExpressionPart>();

            ExtractOperand(input, ref pos, ref expr);
            AssertNotReachedEndOfString(input, pos);

            while (pos < input.Length)
            {
                ExtractOperator(input, ref pos, ref expr);

                ExtractOperand(input, ref pos, ref expr);
            }

            return new Expression(expr);
        }

        private void ExtractOperand(string input, ref int pos, ref List<IExpressionPart> expr)
        {
            double number = extracter.ExtractNumberAt(input, ref pos);
            Operand operand = new Operand(number);
            expr.Add(operand);

            SkipWhiteSpace(input, ref pos);
        }

        private void ExtractOperator(string input, ref int pos, ref List<IExpressionPart> expr)
        {
            IOperation operation = extracter.ExtractOperationAt(input, pos++);
            Operator opr = new Operator(operation);
            expr.Add(opr);

            SkipWhiteSpace(input, ref pos);
        }

        private void SkipWhiteSpace(string input, ref int pos)
        {
            while (pos < input.Length && input[pos] == ' ')
                ++pos;
        }

        private void AssertNotReachedEndOfString(string input, int pos)
        {
            if (input.Length <= pos)
                throw new WrongInputException("Invalid input!");
        }

    }
}
