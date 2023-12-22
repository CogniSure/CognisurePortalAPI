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
        List<SFResult> GetDashboardGraphData(DashboardFilter dashboardFilter, string Type);
        List<SFResult> GetExposerSummary(string Type, string email, string clientId, string submissionId);
    }
}
