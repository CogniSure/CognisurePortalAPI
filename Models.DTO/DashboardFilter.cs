using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DashboardFilter
    {
        public int TopNumber { get; set; }
        public string? CLIENTID { get; set; }
        public string? UserEmailId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
