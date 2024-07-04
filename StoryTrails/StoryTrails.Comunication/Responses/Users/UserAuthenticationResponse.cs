using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryTrails.Comunication.Responses.Users
{
    public class UserAuthenticationResponse
    {
        public string UserLogin { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
    }
}