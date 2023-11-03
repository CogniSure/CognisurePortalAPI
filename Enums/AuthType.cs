using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    public enum AuthType
    {
        none = 0,
        Default = 1,
        AppTOTP = 2,
        EmailOTP = 3
    }
}
