namespace IntegrationTests
{
    public class OSNotSupportedException : Exception
    {
        public OSNotSupportedException()
        {
        }

        public OSNotSupportedException(string? message) : base(message)
        {
        }
    }
}
