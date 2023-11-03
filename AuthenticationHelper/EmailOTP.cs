using MsSqlAdapter.Interface;
using Services.MsSqlServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationHelper
{
    public class EmailOTP : BaseAuthentication
    {
        private readonly IMsSqlDatabase _iMsSqlDatabase;
        public EmailOTP(IMsSqlDatabase iMsSqlDatabase)
        {
            _iMsSqlDatabase = iMsSqlDatabase;
        }
        public override bool ValidateOTP(string Email, string AuthOTP, string secretkey, out string generalMessage, out string technicalMessage)
        {
            return _iMsSqlDatabase.ValidateOrCreateOTP(Email, AuthOTP, "Check", out generalMessage, out technicalMessage);
        }
        public override bool GenerateOTP(string Email, out string generalMessage, out string technicalMessage)
        {
            return _iMsSqlDatabase.ValidateOrCreateOTP(Email, "", "Create", out generalMessage, out technicalMessage);
        }
    }
}
