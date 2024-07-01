namespace StoryTrails.API.Entities
{
    public class Collection
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CollectionName { get; set; } = string.Empty;

        public int CollectionObjective { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
