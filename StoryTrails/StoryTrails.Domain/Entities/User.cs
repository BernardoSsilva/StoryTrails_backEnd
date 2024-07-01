namespace StoryTrails.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;

        public string UserLogin { get; set; } = string.Empty;

        public string UserPassword { get; set; } = string.Empty;
     }
}
