namespace Calculator.Exceptions
{
    public class NoExpressionException : Exception
    {
        public NoExpressionException()
        {
        }

        public NoExpressionException(string? message) : base(message)
        {
        }
    }
}
