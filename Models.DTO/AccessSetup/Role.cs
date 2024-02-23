using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Role
    {
        List<Controller_> _controllers = new List<Controller_>();
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public List<Controller_> Controllers { get { return _controllers; } set { _controllers = value; } }
    }
}
