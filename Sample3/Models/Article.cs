using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Article
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public int? ViewCount { get; set; }
        public int? Sort { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public string SeoDescription { get; set; }
        public int? AddManagerId { get; set; }
        public DateTime? AddTime { get; set; }
        public int? ModifyManagerId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int? IsTop { get; set; }
        public int? IsSlide { get; set; }
        public int? IsRed { get; set; }
        public int? IsPublish { get; set; }
        public int? IsDeleted { get; set; }
    }
}
