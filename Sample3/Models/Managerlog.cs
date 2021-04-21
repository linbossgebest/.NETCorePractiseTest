using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Sample03.Models
{
    public partial class Managerlog
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public int? AddManageId { get; set; }
        public string AddManagerNickName { get; set; }
        public DateTime? AddTime { get; set; }
        public string AddIp { get; set; }
        public string Remark { get; set; }
    }
}
