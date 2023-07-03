namespace Portal.Repository.Dashboard
{
    public interface IDashboardRepository
    {
        string GetDashboardData(); 
    }
    public class DashboardRepository : IDashboardRepository    {
        public string  GetDashboardData()
        {
            return "This is Dashborad response";
        }
    }
}