namespace CampusHire.API.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public string ErrorCode { get; }
        public UnauthorizedException(string message, string errorCode = "UNAUTHORIZED")
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}