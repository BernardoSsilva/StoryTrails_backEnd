namespace StoryTrails.API.Entities
{
    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BookName { get; set; } = string.Empty;

        public string Collection { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public int PagesAmount { get; set; }

        public bool Concluded { get; set; }

    }
}
