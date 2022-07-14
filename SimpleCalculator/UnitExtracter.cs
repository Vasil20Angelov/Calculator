using Calculator.Exceptions;
using Calculator.Operations;

namespace Parser
{
    public class UnitExtracter
    {
        public virtual double ExtractNumberAt(string input, ref int pos)
        {
            string number = "";
            bool containsFloatingPoint = false;

            if (pos < input.Length && IsSign(input[pos]))
            {
                number += input[pos];
                pos++;
            }

            while (pos < input.Length)
            {
                if (IsFloatingPoint(input[pos]))
                {
                    if (containsFloatingPoint)                   
                        break;                    
                    else                   
                        containsFloatingPoint = true;                   
                }            
                else if (!IsNumber(input[pos]))
                {
                    break;
                }

                number += input[pos];
                pos++;
            }

            return ConvertToDouble(number);
        }

        public virtual IOperation ExtractOperationAt(string input, int pos)
        {
            if (pos >= input.Length)
                throw new WrongInputException("Invalid input!");

            OperationFactory factory = new OperationFactory();

            return factory.Create(input[pos]);
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
    }
}
