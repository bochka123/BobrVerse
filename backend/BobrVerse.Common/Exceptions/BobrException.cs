namespace BobrVerse.Common.Exceptions
{
    public class BobrException : Exception
    {
        public BobrException() { }

        public BobrException(string message) : base(message) { }

        public BobrException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
