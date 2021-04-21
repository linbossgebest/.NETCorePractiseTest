using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content1 { get; set; }
        public int Status { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
