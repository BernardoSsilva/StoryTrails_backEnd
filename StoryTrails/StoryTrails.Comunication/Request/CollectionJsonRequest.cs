﻿namespace StoryTrails.Comunication.Request
{
    public class CollectionJsonRequest
    {
        public string CollectionName { get; set; } = string.Empty;

        public int CollectionObjective { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}