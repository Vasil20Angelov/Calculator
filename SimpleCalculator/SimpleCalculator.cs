using Calculator.Exceptions;

namespace Calculator
{
    public class SimpleCalculator
    {
        public double Calculate(Expression expression)
        {
            double result = expression.operation switch
            {
                Operation.Add      => expression.num1 + expression.num2,
                Operation.Subsract => expression.num1 - expression.num2,
                Operation.Multiply => expression.num1 * expression.num2,
                Operation.Divide   => expression.num2 == 0 
                                    ? throw new DivideByZeroException("Cannot divide by zero!") 
                                    : expression.num1 / expression.num2,

                _                  => throw new UndefinedOperationException($"The operation is not supported!")
            };

            if (Double.IsInfinity(result))
            {
                throw new OverflowException("Cannot calculate so big numbers!");
            }

            return result;
        }
    }
}
