using System.Net;

namespace StoryTrails.Communication.Exceptions
{
    public class BadRequestError : AppException
    {
        public string ErrorMessage { get; set; }
        public override int statusCode => (int)HttpStatusCode.BadRequest;
        public BadRequestError(string message) : base(string.Empty) => ErrorMessage = message;

        public override string getError()
        {
            return ErrorMessage;
        }
    }
}
