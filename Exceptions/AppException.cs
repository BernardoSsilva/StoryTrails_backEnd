namespace StoryTrails.Communication.Exceptions
{
    public abstract class AppException : SystemException

    {

        protected AppException(string message) : base(message)
        {

        }

        public abstract int statusCode { get; }
        public abstract string getError();
    }

}
