using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Interface
{
    public interface IDashboardService
    {
        Task<OperationResult<List<SFResult>>> GetDashboardGraph(DashboardFilter dashboardFilter, string Type);
    }
}
