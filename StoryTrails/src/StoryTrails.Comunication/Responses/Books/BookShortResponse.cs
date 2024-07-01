namespace StoryTrails.Comunication.Responses.Books
{
    public class BookShortResponse
    {
        public string Id { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;

        public int PagesAmount { get; set; }
        public bool Concluded { get; set; }

    }
}
