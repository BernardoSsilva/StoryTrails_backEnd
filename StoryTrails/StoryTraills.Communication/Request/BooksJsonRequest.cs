

namespace StoryTrails.Communication.Request
{
    public class BooksJsonRequest
    {
        public string BookName { get; set; } = string.Empty;

        public string Collection { get; set; } = string.Empty;
        public int PagesAmount { get; set; }

        public bool Concluded { get; set; }
    }
}
