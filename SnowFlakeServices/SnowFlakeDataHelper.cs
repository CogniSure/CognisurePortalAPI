using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            DataSet DS=new DataSet();
            switch (Type.ToLower())
            {
                case "countbylob":
                    {
                        DS = Database.DashboardGraph_CountByLOB(dashboardFilter.TopNumber,dashboardFilter.CLIENTID,dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                    }
                    break;
                case "countbybroker":
                    {
                        DS = Database.DashboardGraph_CountByByBroker(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                    }
                    break;
                case "countbycity":
                    {
                        DS = Database.DashboardGraph_CountByCity(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                    }
                    break;
                case "countbystate":
                    {
                        DS = Database.DashboardGraph_CountByState(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                    }
                    break;
                case "countbyindustries":
                    {
                        DS = Database.DashboardGraph_CountByIndustries(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                    }
                    break;

            }
            //var list = Database.DashboardGraph(dashboardFilter, Type);
            return GetAllDashboardGraphData(DS);
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
