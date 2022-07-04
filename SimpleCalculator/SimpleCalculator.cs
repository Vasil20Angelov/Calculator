using Calculator.Exceptions;

namespace Calculator
{
    public class SimpleCalculator
    {
        public double Calculate(Expression expression)
        {
            return expression.operation switch
            {
                Operation.Add      => (double)expression.num1 + expression.num2,
                Operation.Subsract => (double)expression.num1 - expression.num2,
                Operation.Multiply => (double)expression.num1 * expression.num2,
                Operation.Divide   => expression.num2 == 0 
                                    ? throw new DivideByZeroException("Cannot divide by zero!") 
                                    : (double)expression.num1 / expression.num2,

                _                  => throw new UndefinedOperationException($"The operation is not supported!")
            };
        }
    }
}
