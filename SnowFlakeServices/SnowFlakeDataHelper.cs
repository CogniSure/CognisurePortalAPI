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
    public class SnowFlakeDataHelper : ISnowFlakeDataHelper
    {
        private readonly ISnowFlakeDatabase Database;
        public IConfiguration Configuration { get; }
        public SnowFlakeDataHelper(ISnowFlakeDatabase Database, IConfiguration Configuration) : base()
        {
            this.Database = Database;
            this.Configuration = Configuration;
        }
        public List<DataResult> GetDashboardGraphData(DashboardFilter dashboardFilter, string Type)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            switch (Type.ToLower())
            {
                case "countbyturnaroundtime":
                    {
                        DS = Database.DashboardGraph_CountByTurnaroundTime(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("TAT")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID"))
                                    }).ToList();
                        List<DataResult> dashboard = new List<DataResult>();
                        List<DataResult> distinctDashboard = new List<DataResult>();
                        distinctDashboard = lstDasboardgraph.DistinctBy(x => x.Dimension).ToList();
                        foreach (var row in distinctDashboard)
                        {
                            DataResult graph = new DataResult
                            {
                                Category = "Days",
                                Dimension = row.Dimension,
                                Measure = lstDasboardgraph.Where(msr => msr.Dimension == row.Dimension).ToList().Count().ToString()
                            };

                            dashboard.Add(graph);

                        }
                        int a = 1;
                        lstDasboardgraph = dashboard;
                    }
                    break;
                case "countbylob":
                    {
                        DS = Database.DashboardGraph_CountByLOB(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Measure = string.Format("{0}", dataRow.Field<Int64>("COUNTOFSUBMISSIONID"))
                                    }).ToList();
                    }
                    break;
                case "countbybroker":
                    {
                        DS = Database.DashboardGraph_CountByByBroker(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("BROKERNAME")),
                                       Measure = string.Format("{0}", dataRow.Field<Int64>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countbycity":
                    {
                        DS = Database.DashboardGraph_CountByCity(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("CITYNAME")),
                                       Measure = string.Format("{0}", dataRow.Field<Int64>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countbystate":
                    {
                        DS = Database.DashboardGraph_CountByState(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("STATENAME")),
                                       Measure = string.Format("{0}", dataRow.Field<Int64>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countbyindustries":
                    {
                        DS = Database.DashboardGraph_CountByIndustries(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("NAICCODE")),
                                       Measure = string.Format("{0}", dataRow.Field<Int64>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;

            }
            return lstDasboardgraph;
        }
        public List<DataResult> GetSubmissionHeader(string type, string email, string clientId, string subGuid)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            DS = Database.GetSubmissionHeader(clientId, email, subGuid);
            lstDasboardgraph = DS.Tables[0].AsEnumerable()
                        .Select(dataRow => new DataResult
                        {
                            Category = "",
                            Dimension = string.Format("{0}", dataRow.Field<string>("HEADERS")),
                            Measure = string.Format("{0}", dataRow.Field<string>("RESULT"))
                        }).ToList();
            return lstDasboardgraph;
        }
        public List<DataResult> GetExposerSummary(string type, string email, string clientId, string subGuid)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            switch (type.ToLower())
            {
                case "exposure_tiv":
                    {
                        DS = Database.Sub_Exposure_TIV(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<decimal>("SUMOFTIV"))
                                    }).ToList();
                    }
                    break;
                case "exposure_locationcount":
                    {
                        DS = Database.Sub_Exposure_LocationsCount(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFLOCATIONS"))
                                    }).ToList();
                    }
                    break;
                case "exposure_buildingscount":
                    {
                        DS = Database.Sub_Exposure_BuildingsCount(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFBUILDINGS"))
                                    }).ToList();
                    }
                    break;
                case "exposure_constructiontype":
                    {
                        DS = Database.Sub_Exposure_ConstructionType(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("CONSTRUCTIONTYPE")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCONSTRUCTIONTYPE"))
                                    }).ToList();
                    }
                    break;
                case "exposure_occupancytype":
                    {
                        DS = Database.Sub_Exposure_OccupancyType(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("OCCUPANYTYPE")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFOCCUPANYTYPE"))
                                    }).ToList();
                    }
                    break;
                case "exposure_yearbuild":
                    {
                        DS = Database.Sub_Exposure_YearBuild(clientId, email, subGuid);
                        var rawData = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("BUILDINGAGE"))
                                    }).ToList();
                        lstDasboardgraph = new List<DataResult>()
                        {
                            new DataResult { Dimension = "0-5", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 0 && Convert.ToInt64(x.Measure) <= 5).Count().ToString() },
                            new DataResult { Dimension = "6-10", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 6 && Convert.ToInt64(x.Measure) <= 10).Count().ToString() },
                            new DataResult { Dimension = "11-15", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 11 && Convert.ToInt64(x.Measure) <= 15).Count().ToString() },
                            new DataResult { Dimension = "16-25", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 16 && Convert.ToInt64(x.Measure) <= 25).Count().ToString() },
                            new DataResult { Dimension = "26-75", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 26 && Convert.ToInt64(x.Measure) <= 75).Count().ToString() },
                            new DataResult { Dimension = "Greater than 75", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) > 75).Count().ToString() },
                        };

                    }
                    break;
                case "exposure_protectionclass":
                    {
                        DS = Database.Sub_Exposure_ProtectionClass(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("PROTECTIONCLASSCODE")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFPROTECTIONCLASSCODE"))
                                    }).ToList();
                    }
                    break;

            }
            return lstDasboardgraph;
        }
        public List<DataResult> GetLossSummary(string type, string email, string clientId, string subGuid)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            switch (type.ToLower())
            {
                case "loss_claimsbylobbyyear":
                    {
                        DS = Database.Sub_Loss_ClaimByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Dimension = string.Format("{0}", dataRow.Field<long>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredbylobbyyear":
                    {
                        DS = Database.Sub_Loss_IncurredByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<decimal>("SUMOFTIV"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredrangecount":
                    {
                        DS = Database.Sub_Loss_IncurredRangeCount(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("RANGES")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("RANGES")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("INCURREDCOUNT"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimbyclaimtypebyyear":
                    {
                        DS = Database.Sub_Loss_ClaimByClaimTypeByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<long>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredbyclaimtypebyyear":
                    {
                        DS = Database.Sub_Loss_IncurredByClaimTypeByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<long>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimsbyclaimtype":
                    {
                        DS = Database.Sub_Loss_ClaimByClaimType(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimstatus":
                    {
                        DS = Database.Sub_Loss_ClaimStatus(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<long>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_totalincurred":
                    {
                        DS = Database.Sub_Loss_TotalIncurred(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<long>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_toplocations":
                    {
                        DS = Database.Sub_Loss_ClaimByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<long>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<long>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
            }
            return lstDasboardgraph;
        }
    }
}
