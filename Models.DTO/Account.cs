using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Account
    {
        public int ID { get; set; }
        public string AccountName { get; set; }
        public string Carrier { get; set; }
        public string FilesReceived { get; set; }
        public string FilesProcessed { get; set; }
        public int AccountID { get; set; }
        public string FundingType { get; set; }
        public string AccountTypeName { get; set; }
        public string EffectiveDate { get; set; }
        public string MonthYear { get; set; }
        public string LOC { get; set; }
        public string ReportGUID { get; set; }
        public string ReportName { get; set; }
        public string AccountStatus { get; set; }
        public bool IsApproved { get; set; }
        public string FileGUIDs { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string PortalLinks { get; set; }
    }
}
