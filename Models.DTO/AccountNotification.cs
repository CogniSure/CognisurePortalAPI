using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class AccountNotification
    {
        public int NotificationCount { get; set; }
        public int AccountID { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? AddedOn { get; set; }
        public string AddedBy { get; set; }
        public string AccountName { get; set; }
        public string ModifiedBy { get; set; }
    }
}
