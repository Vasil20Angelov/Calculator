namespace Calculator.Exceptions
{
    public class UndefinedOperationException : Exception
    {
        public UndefinedOperationException()
        {
        }

        public UndefinedOperationException(string? message) : base(message)
        {
        }
    }
}
