using Calculator.Exceptions;
using Calculator.ExpressionBuilders;

namespace Calculator
{
    public class SimpleCalculator
    {
        public double Calculate(Expression expression)
        {
            Stack<Operand> operands = new Stack<Operand>();

            for(int i = 0; i < expression.Count; i++)
            {
                if (expression[i] is Operand)
                {
                    Operand value = (Operand)expression[i];
                    operands.Push(value);
                }
                else
                {
                    AssertStackContainsAtleast2Numbers(operands);

                    Operand num2 = operands.Pop();
                    Operand num1 = operands.Pop();
                    Operator op = (Operator)expression[i];

                    Operand result = Calculate(num1, op, num2);

                    operands.Push(result);
                }
            }

            AssertOnly1NumberIsInTheStack(operands);

            return operands.Peek().Value;
        }

        private Operand Calculate(Operand operand1, Operator operation, Operand operand2)
        {
            Operand result = operation.Apply(operand1, operand2);

            if (Double.IsInfinity(result.Value))
            {
                throw new OverflowException("Cannot calculate so big numbers!");
            }

            return result;
        }

        private void AssertOnly1NumberIsInTheStack(Stack<Operand> stack)
        {
            if (stack.Count != 1)
            {
                throw new WrongInputException("The given expression is invalid!");
            }    
        }

        private void AssertStackContainsAtleast2Numbers(Stack<Operand> stack)
        {
            if (stack.Count < 2)
            {
                throw new WrongInputException("The given expression is invalid!");
            }
        }
    }
}
