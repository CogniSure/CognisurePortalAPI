using ApiServices.Interface;
using Services.Common.Interface;
using Services.Repository.Interface;
using Services.SnowFlakeServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factory.Interface
{
    public interface IBusServiceFactory
    {
        IDashboardService DashboardService();
        IConfigurationService ConfigurationService();
        ITokenService TokenService();
        INotificationService NotificationService();
        IUserService UserService();
        IContactUsService ContactUsService();
        ISubmissionService SubmissionService();

        ISubmissionSFService SubmissionSFService();
        IDownloadService DownloadService();
        INewsFeedService NewsFeedService();

        ISubmissionInboxService SubmissionInboxService();
        IChatService ChatService();
    }
}
