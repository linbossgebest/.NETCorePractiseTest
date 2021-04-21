using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
    }
}
