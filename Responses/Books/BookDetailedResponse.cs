namespace StoryTrails.Communication.Responses.Books
{
    public class BookDetailedResponse
    {
        public string Id { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;

        public string Collection { get; set; } = string.Empty;
        public int PagesAmount { get; set; }

        public bool Concluded { get; set; }
    }
}
