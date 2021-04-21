using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Manager
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? LoginCount { get; set; }
        public string LoginLastIp { get; set; }
        public DateTime? LoginLastTime { get; set; }
        public int? AddManagerId { get; set; }
        public DateTime? AddTime { get; set; }
        public int? ModifyManagerId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int? IsLock { get; set; }
        public int? IsDelete { get; set; }
        public string Remark { get; set; }
    }
}
