namespace CampusHire.API.Exceptions
{
    public class BadRequestException : Exception
    {
        public string ErrorCode { get; }
        public BadRequestException(string message, string errorCode = "BAD_REQUEST")
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}