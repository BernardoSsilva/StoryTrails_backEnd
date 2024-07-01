using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Comunication.Request
{
    public class BooksJsonRequest
    {
        public string BookName { get; set; } = string.Empty;

        public string Collection { get; set; } = string.Empty;
        public int PagesAmount { get; set; }

        public bool Concluded { get; set; }
    }
}
