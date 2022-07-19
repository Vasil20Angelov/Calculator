using Calculator.Operations;
using Calculator.ExpressionBuilders;
using Calculator.Exceptions;

namespace Parser
{
    public class Splitter
    {
        private readonly OperationFactory opFactory = new OperationFactory();
        private readonly BracketFactory brFactory = new BracketFactory();
        public virtual List<IExpressionPart> Split(string input)
        {
            string trimmed = RemoveWhiteSpace(input);
            Text text = new Text(trimmed);

            List<IExpressionPart> parts = new List<IExpressionPart>();

            while (text.ValidPosition())
            {
                IExpressionPart part = ExtractPart(text);
                parts.Add(part);
            }

            return parts;
        }
        private IExpressionPart ExtractPart(Text text)
        {
            IExpressionPart part;

            if (IsNumber(text.Current()))
            {
                part = ExtractNumberAndMovePosition(text);
            }
            else if (opFactory.OperationExists(text.Current()))
            {
                part = ExtractOperationAndMovePosition(text);
            }
            else if (IsBracket(text.Current()))
            {
                part = ExtractBracketAndMovePosition(text);
            }
            else
            {
                throw new WrongInputException("Invalid input!");
            }

            return part;
        }

        private Operand ExtractNumberAndMovePosition(Text input)
        {
            string number = "";
            bool containsFloatingPoint = false;

            if (input.ValidPosition() && IsSign(input.Current()))
            {
                number += input.Current();
                input.MovePosByOne();
            }

            while (input.ValidPosition())
            {
                if (IsFloatingPoint(input.Current()))
                {
                    if (containsFloatingPoint)                   
                        break;                    
                    else                   
                        containsFloatingPoint = true;                   
                }            
                else if (!IsNumber(input.Current()))
                {
                    break;
                }

                number += input.Current();
                input.MovePosByOne();
            }

            double converted = ConvertToDouble(number);

            return new Operand(converted);
        }

        private Operator ExtractOperationAndMovePosition(Text input)
        {
            input.AssertCorrectPossition();
            IOperation operation = opFactory.Create(input.Current());
            input.MovePosByOne();

            return new Operator(operation);
        }

        private IBracket ExtractBracketAndMovePosition(Text input)
        {
            input.AssertCorrectPossition();
            IBracket bracket = brFactory.Create(input.Current());
            input.MovePosByOne();

            return bracket;
        }

        private double ConvertToDouble(string input)
        {
            double number = Double.Parse(input);

            if (double.IsInfinity(number))
            {
                throw new OverflowException("The entered numbers are too big!");
            }

            return number;
        }
        private bool IsFloatingPoint(char c) => c == ',';
        private bool IsNumber(char c) => c >= '0' && c <= '9';
        private bool IsSign(char c) => c == '+' || c == '-';
        private bool IsBracket(char c) => c == '(' || c == ')';
        private string RemoveWhiteSpace(string input) => String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
    }
}
