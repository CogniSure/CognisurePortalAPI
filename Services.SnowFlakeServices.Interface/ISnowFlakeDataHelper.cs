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
        List<DataResult> GetDashboardGraphData(DashboardFilter dashboardFilter, string Type);
        List<DataResult> GetSubmissionHeader(string type, string email, string clientId, string subGuid);
        List<DataResult> GetExposerSummary(string Type, string email, string clientId, string submissionId);
        List<DataResult> GetLossSummary(string type, string email, string clientId, string subGuid);
        Submission GetSubmissionSummaryByLOB(string type, string email, string clientId, string subGuid);
        
    }
}
