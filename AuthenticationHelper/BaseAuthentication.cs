using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationHelper
{
    public abstract class BaseAuthentication
    {
        public virtual bool ValidateOTP(string Email, string AuthOTP, string secretkey, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            return false;
        }
        public virtual bool GenerateOTP(string Email, out string generalMessage, out string technicalMessage)
        {

            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            return false;
        }
        public virtual string GenerateqrCodeUri(string email, string appName, string QRCodefilePath, string secret)
        {
            return string.Empty;
        }
    }
}
