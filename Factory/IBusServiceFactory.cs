using Services.Common.Interface;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MsSqlServices.Interface
{
    public interface IBusServiceFactory
    {
        ITokenService TokenService();
        INotificationService NotificationService();
        IUserService UserService();
        IContactUsService ContactUsService();
        //IWidgetService WidgetService();
        //I2FAService TwoFactorAuthenticationService();
        IDownloadService DownloadService();
        INewsFeedService NewsFeedService();
    }
}
