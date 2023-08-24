using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Submission360
    {
        public string id { get; set; }
        public bool status { get; set; }
        public bool returnCode { get; set; }
        public string fileName { get; set; }
        public string message { get; set; }
        public string data { get; set; }
    }

}
