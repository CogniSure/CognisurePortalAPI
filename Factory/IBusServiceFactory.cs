using Services.Common.Interface;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factory.Interface
{
    public interface IBusServiceFactory
    {
        ITokenService TokenService();
        INotificationService NotificationService();
        IUserService UserService();
        IContactUsService ContactUsService();
        ISubmissionService SubmissionService();
        //I2FAService TwoFactorAuthenticationService();
        IDownloadService DownloadService();
        INewsFeedService NewsFeedService();
    }
}
