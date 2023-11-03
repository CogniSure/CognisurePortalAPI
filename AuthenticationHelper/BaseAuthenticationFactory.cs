using Models.Enums;
using MsSqlAdapter.Interface;
using Services.MsSqlServices.Interface;

namespace AuthenticationHelper
{
    public class BaseAuthenticationFactory
    {
        private readonly IMsSqlDatabase _iMsSqlDatabase;
        public BaseAuthenticationFactory(IMsSqlDatabase iMsSqlDatabase)
        {
            _iMsSqlDatabase = iMsSqlDatabase;
        }
        public BaseAuthentication GetAuthentication(int objAuthType)
        {
            if (objAuthType == Convert.ToInt32(AuthType.EmailOTP))
            {
                return new EmailOTP(_iMsSqlDatabase);
            }
            else if (objAuthType == Convert.ToInt32(AuthType.AppTOTP))
            {
                return new AppTOTP(_iMsSqlDatabase);
            }
            return null;
        }
    }
}
