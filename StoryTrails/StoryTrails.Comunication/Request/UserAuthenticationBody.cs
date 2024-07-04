

namespace StoryTrails.Comunication.Request
{
    public class UserAuthenticationBody
    {
        public string UserLogin { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}