using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class InboxFilter
    {
        public int UserId { get; set; }
        public int UploadedUserID { get; set; }
        public int SubmissionId { get; set; }
        public string MessageId { get; set; }
        public string keyword { get; set; }
        public Nullable<DateTime> SubmissionFromDate { get; set; }
        public Nullable<DateTime> SubmissionToDate { get; set; }
        public int FileReceivedChanelId { get; set; }
        public int AddedById { get; set; }
    }
}
