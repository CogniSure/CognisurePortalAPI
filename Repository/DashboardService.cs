using Services.Repository.Interface;

namespace Portal.Repository.Dashboard
{
    
    public class DashboardService : IDashboardRepository    {
        public string  GetDashboardData()
        {
            return "This is Dashborad response";
        }
    }
}