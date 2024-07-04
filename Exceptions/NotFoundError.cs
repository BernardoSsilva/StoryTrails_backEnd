using System.Net;

namespace StoryTrails.Communication.Exceptions
{
    public class NotFoundError : AppException
    {
        public string ErrorMessage { get; set; }
        public override int statusCode => (int)HttpStatusCode.NotFound;
        public NotFoundError(string message) : base(string.Empty) => ErrorMessage = message;

        public override string getError()
        {
            return ErrorMessage;
        }
    }
}
