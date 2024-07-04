using System.Net;

namespace StoryTrails.Communication.Exceptions
{
    public class ConflictError : AppException
    {
        public string ErrorMessage { get; set; }
        public override int statusCode => (int)HttpStatusCode.Conflict;
        public ConflictError(string message) : base(string.Empty) => ErrorMessage = message;

        public override string getError()
        {
            return ErrorMessage;
        }
    }
}
