using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Managerrole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int? RoleType { get; set; }
        public int? IsSystem { get; set; }
        public int? AddManagerId { get; set; }
        public DateTime? AddTime { get; set; }
        public int? ModifyManagerId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int? IsDelete { get; set; }
        public string Remark { get; set; }
    }
}
