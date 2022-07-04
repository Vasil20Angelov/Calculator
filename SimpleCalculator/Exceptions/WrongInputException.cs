namespace Calculator.Exceptions
{
    public class WrongInputException : Exception
    {
        public WrongInputException()
        {
        }

        public WrongInputException(string? message) : base(message)
        {
        }
    }
}
