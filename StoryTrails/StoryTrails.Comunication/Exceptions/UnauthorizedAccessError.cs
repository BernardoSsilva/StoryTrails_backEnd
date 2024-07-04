

using System.Net;

namespace StoryTrails.Comunication.Exceptions
{
    public class UnauthorizedAccessError : AppException
    {
        public string ErrorMessage { get; set; }
        public override int statusCode => (int)HttpStatusCode.Unauthorized;
        public UnauthorizedAccessError(string message) : base(string.Empty) => ErrorMessage = message;

        public override string getError()
        {
            return ErrorMessage;
        }
    }
}