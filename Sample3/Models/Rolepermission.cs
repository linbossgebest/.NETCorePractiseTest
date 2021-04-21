using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Rolepermission
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
        public string Permission { get; set; }
    }
}
