using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SnowFlakeServices.Interface
{
    public interface ISnowFlakeDataHelper
    {
        List<DashboardGraph> GetDashboardGraphData(DashboardFilter dashboardFilter, string Type);
    }
}
