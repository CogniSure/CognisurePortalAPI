using Models.DTO;
using System.Data;

namespace SnowFlakeAdapter.Interface
{
    public interface ISnowFlakeDatabase
    {
        DataSet SampleTest();
        DataSet DashboardGraph(DashboardFilter dashboardFilter, string Type);
    }
}
