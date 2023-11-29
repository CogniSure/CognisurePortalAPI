using Microsoft.Extensions.Configuration;
using Models.DTO;
using Services.SnowFlakeServices.Interface;
using SnowFlakeAdapter.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFlakeServices
{
    public class SnowFlakeDataHelper: ISnowFlakeDataHelper
    {
        private readonly ISnowFlakeDatabase Database;
        public IConfiguration Configuration { get; }
        public SnowFlakeDataHelper(ISnowFlakeDatabase Database, IConfiguration Configuration) : base()
        {
            this.Database = Database;
            this.Configuration = Configuration;
        }
        public List<DashboardGraph> GetDashboardGraphData(DashboardFilter dashboardFilter, string Type)
        {
            var list = Database.DashboardGraph(dashboardFilter, Type);
            return GetAllDashboardGraphData(list);
        }
        private static List<DashboardGraph> GetAllDashboardGraphData(DataSet dst)
        {
            DashboardGraph DP;
            CultureInfo culture = new CultureInfo("en-US");
            var SubmissionList = new List<DashboardGraph>();
            if (dst.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    DP = new DashboardGraph();
                    var ObjSubmission = new DashboardGraph
                    {

                        Dimension = string.Format("{0}", dr["NAICCODE"]),
                        Measure = string.Format("{0}", dr["COUNTOFSUBMISSIONID"]),
                       
                    };
                   

                    SubmissionList.Add(ObjSubmission);
                }
            }
            return SubmissionList;
        }
    }
}
