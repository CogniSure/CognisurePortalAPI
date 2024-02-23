using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Action_
    {
        public int ActionID { get; set; }
        public string ActionName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
