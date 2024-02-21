using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SubmissionMessage
    {
        public string MessageReceivedFromEmail { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
    }
}
