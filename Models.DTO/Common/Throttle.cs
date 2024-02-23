using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ThrottleData
    {
        public int UserID { get; set; }
        public int RequestLimit { get; set; }
        public int TimeoutInSeconds { get; set; }
    }
}
