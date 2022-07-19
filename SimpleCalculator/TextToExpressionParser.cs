using Calculator.Exceptions;
using Calculator.ExpressionBuilders;
using Calculator.Operations;

namespace Parser
{
    public class TextToExpressionParser
    {
        private readonly Splitter splitter;
        public TextToExpressionParser(Splitter splitter) { this.splitter = splitter; }

        public Expression Parse(string? input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new NoExpressionException("Invalid input!");
            }

            List<IExpressionPart> parts = splitter.Split(input);
            ConvertOperatorsIntoNumberSigns(parts);

            return new Expression(parts);
        }

        private void ConvertOperatorsIntoNumberSigns(List<IExpressionPart> parts)
        {
            if (parts.Count < 2)
            {
                return;
            }    

            if (IsSignFollowedByOperand(parts, 0))
            {
                Replace(parts, 0);
            }

            for (int i = 1; i < parts.Count - 1; i++)
            {
                if (IsSignFollowedByOperand(parts, i) && parts[i-1] is Operator)
                {
                    Replace(parts, i);
                }
            }
        }

        private bool IsSignFollowedByOperand(List<IExpressionPart> parts, int index)
        {
            return IsSign(parts[index]) && parts[index + 1] is Operand;
        }
        private bool IsSign(IExpressionPart part)
        {
            if (part is Operator op)
            {
                return op.Type == Substraction.Symbol || op.Type == Addition.Symbol;
            }

            return false;
        }

        private void Replace(List<IExpressionPart> parts, int index)
        {
            Operator operation = (Operator)parts[index];
            parts.RemoveAt(index);
            
            if (operation.Type == Substraction.Symbol)
            {
                double number = ((Operand)parts[index]).Value * (-1);
                parts[index] = new Operand(number);
            }
        }

    }
}
