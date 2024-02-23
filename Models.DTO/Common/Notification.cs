using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string NotificationName { get; set; }
        public string Description { get; set; }
        public int AlertTypeID { get; set; }
        public bool IsNotificationRead { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? AddedOn { get; set; }
        public int NotificationCount { get; set; }
        public string AccountName { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
