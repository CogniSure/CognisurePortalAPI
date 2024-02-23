using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Controller_
    {
        List<Action_> _actions = new List<Action_>();
        public int ControllerID { get; set; }
        public string ControllerName { get; set; }
        public bool IsActive { get; set; }
        public List<Action_> Actions { get { return _actions; } set { _actions = value; } }
    }
}
