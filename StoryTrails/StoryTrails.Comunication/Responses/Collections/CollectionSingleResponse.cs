namespace StoryTrails.Comunication.Responses.Collections
{
    public class CollectionSingleResponse
    {
        public string Id { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;

        public int CollectionObjective { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
