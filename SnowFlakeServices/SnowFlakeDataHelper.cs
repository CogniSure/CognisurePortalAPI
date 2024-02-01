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

        public List<SubmissionFile> GetSubmissionFiles(string email, string clientId, string subGuid)
        {
            List<SubmissionFile> lstDasboardgraph = new List<SubmissionFile>();
            DataSet DS = new DataSet();
            DS = Database.Sub_Submission_Files(clientId, email, subGuid);
            lstDasboardgraph = DS.Tables[0].AsEnumerable()
                        .Select(dataRow => new SubmissionFile
                        {
                            SlNo = string.Format("{0}", dataRow.Field<string>("SLNO")),
                            FileName = string.Format("{0}", dataRow.Field<string>("FILENAME")),
                            Type = string.Format("{0}", dataRow.Field<string>("TYPE")),
                            LineOfBusiness = string.Format("{0}", dataRow.Field<string>("LOB")),
                            Status = string.Format("{0}", dataRow.Field<string>("STATUS"))
                        }).ToList();
            return lstDasboardgraph;
        }

        private List<DataResult> GetPercentage(List<DataResult> inputData)
        {

            List<DataResult> data = new List<DataResult>();
            int AllSubmissionCount = inputData.Where(i=>i.Dimension != "SubmissionIdCount").Sum(x => Convert.ToInt32(x.Measure));
            foreach (var dataRow in inputData) {
                if(dataRow.Dimension == "SubmissionIdCount")
                {
                    data.Add(dataRow);
                }
                else
                {
                    int msr = Convert.ToInt32(dataRow.Measure);
                    DataResult res = new DataResult
                    {
                        Category = dataRow.Category,
                        Dimension = dataRow.Dimension,
                        Measure = (msr * 100 / AllSubmissionCount).ToString()
                    };
                    data.Add(res);
                }
            }
            return data;
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
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        List<DataResult> graphData = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("TAT")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                    }).ToList();
                        //List<DataResult> dashboard = new List<DataResult>();
                        //List<DataResult> distinctDashboard = new List<DataResult>();

                        if(graphData != null && graphData.Count > 0)
                        {

                            var zeroToFiveMin = graphData.Where(x => x.Dimension == "0 to 5 Min").FirstOrDefault();
                            var fiveToTenMin = graphData.Where(x => x.Dimension == "5 to 10 min").FirstOrDefault();
                            var tenToThirtyMin = graphData.Where(x => x.Dimension == "10 to 30 Min").FirstOrDefault();
                            var thirtyToOneHour = graphData.Where(x => x.Dimension == "30 min to 1 hour").FirstOrDefault();
                            var oneToOneDay = graphData.Where(x => x.Dimension == "1 hr to 1 Day").FirstOrDefault();
                            var moreThanOneDay = graphData.Where(x => x.Dimension == "> 1 Day").FirstOrDefault();

                            lstDasboardgraph = new List<DataResult> { 
                                new DataResult { Category = "Days", Dimension = "0 - 5min", Measure = zeroToFiveMin != null ? zeroToFiveMin.Measure.ToString():"0" },
                                new DataResult { Category = "Days", Dimension = "5 - 10min", Measure = fiveToTenMin != null ? fiveToTenMin.Measure.ToString():"0" },
                                new DataResult { Category = "Days", Dimension = "10 - 30min", Measure = tenToThirtyMin != null ? tenToThirtyMin.Measure.ToString():"0" },
                                new DataResult { Category = "Days", Dimension = "30min - 1hr", Measure = thirtyToOneHour != null ? thirtyToOneHour.Measure.ToString():"0" },
                                new DataResult { Category = "Days", Dimension = "1hr - 1day", Measure = oneToOneDay != null ? oneToOneDay.Measure.ToString():"0"  },
                                new DataResult { Category = "Days", Dimension = "> 1day", Measure = moreThanOneDay != null ? moreThanOneDay.Measure.ToString():"0"  },
                            };
                        }
                        //distinctDashboard = lstDasboardgraph.DistinctBy(x => x.Dimension).ToList();

                        //foreach (var row in distinctDashboard)
                        //{
                        //    var msrValue = lstDasboardgraph.Where(msr => msr.Dimension == row.Dimension).ToList().Count().ToString();

                        //    DataResult graph = new DataResult
                        //    {
                        //        Category = "Days",
                        //        Dimension = row.Dimension + " min",
                        //        Measure = msrValue
                        //    };

                        //    dashboard.Add(graph);

                        //}
                        //lstDasboardgraph = dashboard;
                    }
                    break;
                case "countbylob":
                    {
                        DS = Database.DashboardGraph_CountByLOB(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                    }).ToList();
                        lstDasboardgraph = GetPercentage(data);
                        lstDasboardgraph = lstDasboardgraph.OrderByDescending(x => Convert.ToInt32(x.Measure)).Take(5).ToList();
                        
                    }
                    break;
                case "countbybroker":
                    {
                        DS = Database.DashboardGraph_CountByByBroker(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("BROKERNAME")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                        lstDasboardgraph = GetPercentage(lstDasboardgraph);
                        lstDasboardgraph = lstDasboardgraph.OrderByDescending(x => Convert.ToInt32(x.Measure)).Take(5).ToList();
                    }
                    break;
                case "countbycity":
                    {
                        DS = Database.DashboardGraph_CountByCity(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("CITYNAME")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                        lstDasboardgraph = GetPercentage(lstDasboardgraph);
                        lstDasboardgraph = lstDasboardgraph.OrderByDescending(x => Convert.ToInt32(x.Measure)).Take(5).ToList();
                    }
                    break;
                case "countbystate":
                    {
                        DS = Database.DashboardGraph_CountByState(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("STATENAME")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                        lstDasboardgraph = GetPercentage(lstDasboardgraph);
                        lstDasboardgraph = lstDasboardgraph.OrderByDescending(x => Convert.ToInt32(x.Measure)).Take(5).ToList();
                    }
                    break;
                case "countbyindustries":
                    {
                        DS = Database.DashboardGraph_CountByIndustries(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("NAICCODE")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                        lstDasboardgraph = GetPercentage(lstDasboardgraph);
                        lstDasboardgraph = lstDasboardgraph.OrderByDescending(x => Convert.ToInt32(x.Measure)).Take(5).ToList();
                    }
                    break;
                case "countofsubmissionprofileandvolume":
                    {
                        DS = Database.DashboardGraph_CountOfSubmissionId(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        //DS = Database.DashboardGraph_CountOfSubmissionId(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                        //    Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        DataSet DSDocType = new DataSet();
                        DSDocType = Database.DashboardGraph_CountOfDocType(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        List<DataResult> dataResult = new List<DataResult>();
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = "SubmissionIdCount",
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                        dataResult.AddRange(
                            DSDocType.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("DOCUMENTTYPE")) == ""?"Others": string.Format("{0}", dataRow.Field<string>("DOCUMENTTYPE")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList()
                            );
                        foreach(var row in dataResult)
                        {
                            if(row.Dimension.ToLower().Replace(" ", string.Empty) == "lossrun")
                            {
                                var tempdata = lstDasboardgraph.Find(x=>x.Dimension == "Loss Run");
                                if (tempdata != null)
                                {
                                    tempdata.Measure = (Convert.ToInt32(tempdata.Measure) + Convert.ToInt32(row.Measure)).ToString();
                                }
                                else
                                {
                                    lstDasboardgraph.Add(new DataResult { Dimension = "Loss Run", Measure = row.Measure });
                                }
                            }
                            else if (row.Dimension.ToLower().Replace(" ", string.Empty) == "sov" 
                                || row.Dimension.ToLower().Replace(" ", string.Empty) == "statement" 
                                || row.Dimension.ToLower().Replace(" ", string.Empty) == "schedules")
                            {
                                var tempdata = lstDasboardgraph.Find(x => x.Dimension == "SOV");
                                if (tempdata != null)
                                {
                                    tempdata.Measure = (Convert.ToInt32(tempdata.Measure) + Convert.ToInt32(row.Measure)).ToString();
                                }
                                else
                                {
                                    lstDasboardgraph.Add(new DataResult { Dimension = "SOV", Measure = row.Measure });
                                }
                            }
                            else if (row.Dimension.ToLower().Replace(" ", string.Empty) == "acord")
                            {
                                var tempdata = lstDasboardgraph.Find(x => x.Dimension == "ACORD");
                                if (tempdata != null)
                                {
                                    tempdata.Measure = (Convert.ToInt32(tempdata.Measure) + Convert.ToInt32(row.Measure)).ToString();
                                }
                                else
                                {
                                    lstDasboardgraph.Add(new DataResult { Dimension = "ACORD", Measure = row.Measure });
                                }
                            }
                            else
                            {
                                var tempdata = lstDasboardgraph.Find(x => x.Dimension == "Others");
                                if (tempdata != null)
                                {
                                    tempdata.Measure = (Convert.ToInt32(tempdata.Measure) + Convert.ToInt32(row.Measure)).ToString();
                                }
                                else
                                {
                                    lstDasboardgraph.Add(new DataResult { Dimension = "Others", Measure = row.Measure });
                                }
                            }
                        }
                        lstDasboardgraph = GetPercentage(lstDasboardgraph); 
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("SUMOFTIV"))
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFLOCATIONS"))
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFBUILDINGS"))
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCONSTRUCTIONTYPE"))
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFOCCUPANYTYPE"))
                                    }).ToList();
                    }
                    break;
                case "exposure_yearbuild":
                    {
                        DS = Database.Sub_Exposure_YearBuild(clientId, email, subGuid);
                        var rawData = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("RANGES")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFBUIDLINGAGE"))
                                    }).ToList();
                        lstDasboardgraph = rawData;
                        //    new List<DataResult>()
                        //{
                        //    new DataResult { Dimension = "0-5", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 0 && Convert.ToInt64(x.Measure) <= 5).Count().ToString() },
                        //    new DataResult { Dimension = "6-10", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 6 && Convert.ToInt64(x.Measure) <= 10).Count().ToString() },
                        //    new DataResult { Dimension = "11-15", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 11 && Convert.ToInt64(x.Measure) <= 15).Count().ToString() },
                        //    new DataResult { Dimension = "16-25", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 16 && Convert.ToInt64(x.Measure) <= 25).Count().ToString() },
                        //    new DataResult { Dimension = "26-75", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 26 && Convert.ToInt64(x.Measure) <= 75).Count().ToString() },
                        //    new DataResult { Dimension = "Greater than 75", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) > 75).Count().ToString() },
                        //};

                    }
                    break;
                case "exposure_protectionclass":
                    {
                        DS = Database.Sub_Exposure_ProtectionClass(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("PROTECTIONCLASSCODE")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFPROTECTIONCLASSCODE"))
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
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredbylobbyyear":
                    {
                        DS = Database.Sub_Loss_IncurredByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("INCURRED"))
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("INCURREDCOUNT"))
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
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
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
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("INCURRED"))
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
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimstatus":
                    {
                        DS = Database.Sub_Loss_ClaimStatus(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = "",
                                        Dimension = string.Format("{0}", dataRow.Field<string>("CLAIMSTATUS")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("PERCENTAGEOFCLAIMSTATUS"))
                                    }).ToList();
                    }
                    break;
                case "loss_totalincurred":
                    {
                        DS = Database.Sub_Loss_TotalIncurred(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = "",
                                        Dimension = string.Format("{0}", dataRow.Field<string>("FIELDNAMES")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("AMOUNTS"))
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
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
            }
            return lstDasboardgraph;
        }

        public Submission GetSubmissionSummaryByLOB(string type, string email, string clientId, string subGuid)
        {
            Submission submissionData = new Submission();
            DataSet DS = new DataSet();
            switch (type.ToLower())
            {
                case "sub_agencies_all":
                    {
                        DS = Database.Sub_Summary_Agency(clientId, email, subGuid);
                        Agency agency = new Agency();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            var key = data.Field<string>("HEADERS").ToLower();
                            switch (key)
                            {
                                case "producerfullname":
                                    agency.AgencyName = data.Field<string>("RESULT");
                                    break;
                                case "insurerproduceridentifier": 
                                    agency.Producer = data.Field<string>("RESULT");
                                    break;
                            }
                            var v = data;
                        });
                        submissionData.Agency = agency;
                    }
                    break;
                case "sub_businessoperations_all":
                    {
                        DS = Database.Sub_Summary_BusinessOperations(clientId, email, subGuid);
                        BusinessOperation bussOperation = new BusinessOperation();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            var key = data.Field<string>("HEADERS").ToLower();
                            switch (key)
                            {
                                case "namedinsurednaiccode":
                                    bussOperation.Naics = data.Field<string>("RESULT");
                                    break;
                                //case "insurerproduceridentifier":
                                //    agency.Producer = data.Field<string>("RESULT");
                                //    break;
                            }
                            var v = data;
                        });
                        submissionData.BusinessOperation = bussOperation;
                    }
                    break;
                case "sub_totallosses_all":
                    {
                        DS = Database.Sub_Summary_TotalLosses(clientId, email, subGuid);
                        submissionData.TotalLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                        submissionData.TotalLosses.Add(new SubmissionLosses { Year = "2020", GrossAmount = "100", NoOfOpenClaims = "10", TotalNoOfClaims = "110" });
                        submissionData.TotalLosses.Add(new SubmissionLosses { Year = "2021", GrossAmount = "200", NoOfOpenClaims = "20", TotalNoOfClaims = "120" });
                        submissionData.TotalLosses.Add(new SubmissionLosses { Year = "2022", GrossAmount = "300", NoOfOpenClaims = "30", TotalNoOfClaims = "130" });
                        submissionData.TotalLosses.Add(new SubmissionLosses { Year = "2023", GrossAmount = "400", NoOfOpenClaims = "40", TotalNoOfClaims = "140" });
                    }
                    break;
                case "sub_exposure_property":
                    {
                        DS = Database.Sub_Summary_Property_Exposure(clientId, email, subGuid);
                        
                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new Exposure
                                    {
                                        Total = string.Format("{0}", dataRow.Field<string>("TIV")) == ""?"0": string.Format("{0}", dataRow.Field<string>("TIV")),
                                        ExposureValue1 = string.Format("{0}", dataRow.Field<string>("COUNTOFBUILDINGS")),
                                        ExposureValue2 = string.Format("{0}", dataRow.Field<string>("COUNTOFLOCATIONS")),
                                        ExposureValue3 = string.Format("{0}", dataRow.Field<string>("COUNTOFSTATES"))
                                    });
                        if (data != null)
                        {
                            submissionData.PropertyExposure = data.FirstOrDefault();
                        }
                    }
                    break;
                case "sub_coverage_property":
                    {
                        DS = Database.Sub_Summary_Property_Coverages(clientId, email, subGuid);
                        List<Coverages> propertyCoverages = new List<Coverages>();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();
                            if (data.Field<string>("BUILDINGLIMIT") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Building",
                                    CoverageValue = Convert.ToDouble(string.Format("{0}", data.Field<string>("BUILDINGLIMIT"))).ToString("#,##0"),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Building",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("CONTENTLIMIT") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Content",
                                    CoverageValue = Convert.ToDouble(string.Format("{0}", data.Field<string>("CONTENTLIMIT"))).ToString("#,##0"),
                                    CoverageType = ""
                                });

                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Content",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("BUSINESSINCOMELIMIT") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Business Income",
                                    CoverageValue = Convert.ToDouble(string.Format("{0}", data.Field<string>("BUSINESSINCOMELIMIT"))).ToString("#,##0"),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Business Income",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("OTHERLIMIT") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Others",
                                    CoverageValue = Convert.ToDouble(string.Format("{0}", data.Field<string>("OTHERLIMIT"))).ToString("#,##0"),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Others",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }


                        });
                        if (propertyCoverages != null && propertyCoverages.Count > 0)
                        {
                            submissionData.PropertyCoverages = propertyCoverages;
                        }
                        else //if (DS.Tables[0].AsEnumerable().ToList().Count == 0)
                        {
                            submissionData.AutoCoverages = new List<Coverages>
                            {
                                new Coverages { CoverageName = "Building", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Content", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Business Income", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Others", CoverageValue = "0", CoverageType = "" }
                            };

                        }
                    }
                    break;
                case "sub_losses_property":
                    {
                        DS = Database.Sub_Summary_Property_Losses(clientId, email, subGuid);
                        submissionData.PropertyLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "sub_exposure_auto":
                    {
                        DS = Database.Sub_Summary_Auto_Exposure(clientId, email, subGuid);

                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new Exposure
                                    {
                                        Total = string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFVEHICLES")) == "" ? "0" : string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFVEHICLES")),
                                        ExposureValue1 = string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFDRIVERS")),
                                        ExposureValue2 = "NA",
                                        ExposureValue3 = string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFBODYTYPE"))
                                    });
                        if (data != null)
                        {
                            submissionData.AutoExposure = data.ToList();
                        }
                    }
                    break;
                case "sub_coverage_auto":
                    {
                        DS = Database.Sub_Summary_Auto_Coverages(clientId, email, subGuid);
                        List<Coverages> propertyCoverages = new List<Coverages>();
                        
                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();
                            if (data.Field<string>("VINNUM") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "VIN #",
                                    CoverageValue = string.Format("{0}", data.Field<string>("VINNUM")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "VIN #",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("VEHICLETYPE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Vehicle Type",
                                    CoverageValue = string.Format("{0}", data.Field<string>("VEHICLETYPE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Vehicle Type",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("LICENCESTATE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Licences State",
                                    CoverageValue = string.Format("{0}", data.Field<string>("LICENCESTATE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Licences State",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }

                        });
                        if (propertyCoverages != null && propertyCoverages.Count > 0)
                        {
                            submissionData.AutoCoverages = propertyCoverages;
                        }
                        else //if (DS.Tables[0].AsEnumerable().ToList().Count == 0)
                        {
                            submissionData.AutoCoverages = new List<Coverages>
                            {
                                new Coverages { CoverageName = "VIN #", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Vehicle Type", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Licences State", CoverageValue = "0", CoverageType = "" }
                            };
                            
                        }
                    }
                    break;
                case "sub_losses_auto":
                    {
                        DS = Database.Sub_Summary_Auto_Losses(clientId, email, subGuid);
                        submissionData.AutoLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "sub_exposure_wc":
                    {
                        DS = Database.Sub_Summary_WC_Exposure(clientId, email, subGuid);

                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new Exposure
                                    {
                                        Total = "0",//string.Format("{0}", dataRow.Field<string>("TIV")),
                                        ExposureValue1 = string.Format("{0}", dataRow.Field<string>("CLASSCODE")),
                                        ExposureValue2 = string.Format("{0}", dataRow.Field<string>("CLASSCODEDESCRIPTION")),
                                        ExposureValue3 = string.Format("{0}", dataRow.Field<string>("PAYROLL"))
                                    });
                        if (data != null)
                        {
                            submissionData.WorkersCompExposure = data.ToList();
                        }
                    }
                    break;
                case "sub_coverage_wc":
                    {
                        DS = Database.Sub_Summary_WC_PayRollEmployee(clientId, email, subGuid);
                        List<Coverages> propertyCoverages = new List<Coverages>();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();
                            if (data.Field<string>("TOTALPAYROLL") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Total Payroll",
                                    CoverageValue = string.Format("{0}", data.Field<string>("TOTALPAYROLL")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Total Payroll",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("COVEREDSTATES") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Covered States",
                                    CoverageValue = string.Format("{0}", data.Field<string>("COVEREDSTATES")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Covered States",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("FULLTIMEEMPLOYEECOUNT") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "# Full Time Employee",
                                    CoverageValue = string.Format("{0}", data.Field<string>("FULLTIMEEMPLOYEECOUNT")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "# Full Time Employee",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("PARTTIMEEMPLOYEECOUNT") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "# Part Time Employee",
                                    CoverageValue = string.Format("{0}", data.Field<string>("PARTTIMEEMPLOYEECOUNT")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "# Part Time Employee",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }

                        });
                        if (propertyCoverages != null && propertyCoverages.Count>0)
                        {
                            submissionData.WorkersCompCoverages = propertyCoverages;
                        }
                        else //if (DS.Tables[0].AsEnumerable().ToList().Count == 0)
                        {
                            submissionData.GeneralLiablityCoverages = new List<Coverages>
                            {
                                new Coverages { CoverageName = "Total Payroll", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Covered States", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "# Full Time Employee", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "# Part Time Employee", CoverageValue = "0", CoverageType = "" }
                            };

                        }
                    }
                    break;
                case "sub_losses_wc":
                    {
                        DS = Database.Sub_Summary_WC_Losses(clientId, email, subGuid);
                        submissionData.WorkersCompLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "sub_exposure_gl":
                    {
                        DS = Database.Sub_Summary_GL_ScheduleOfHazards(clientId, email, subGuid);

                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new Exposure
                                    {
                                        Total = "0",//string.Format("{0}", dataRow.Field<string>("TIV")),
                                        ExposureValue1 = string.Format("{0}", dataRow.Field<string>("CLASSCODE")),
                                        ExposureValue2 = string.Format("{0}", dataRow.Field<string>("EXPOSURE")),
                                        ExposureValue3 = string.Format("{0}", dataRow.Field<string>("EXPOSURETYPE"))
                                    });
                        if (data != null)
                        {
                            submissionData.GeneralLiablityExposure = data.ToList();
                        }
                    }
                    break;
                case "sub_coverage_gl":
                    {
                        DS = Database.Sub_Summary_GL_Coverage(clientId, email, subGuid);
                        List<Coverages> propertyCoverages = new List<Coverages>();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();
                            if (data.Field<string>("EACHOCCURRENCE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Each Occurrence",
                                    CoverageValue = string.Format("{0}", data.Field<string>("EACHOCCURRENCE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Each Occurrence",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("AGGREGATE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Aggregate",
                                    CoverageValue = string.Format("{0}", data.Field<string>("AGGREGATE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Aggregate",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("PRODUCTSCOMPLETEDOPERATIONSAGGREGATE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Products-Completed Operations Aggregate",
                                    CoverageValue = string.Format("{0}", data.Field<string>("PRODUCTSCOMPLETEDOPERATIONSAGGREGATE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Products-Completed Operations Aggregate",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("PERSONALANDADVERTISINGINJURY") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Personal and Advertising Injury",
                                    CoverageValue = string.Format("{0}", data.Field<string>("PERSONALANDADVERTISINGINJURY")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Personal and Advertising Injury",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("FIREDAMAGE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Fire Damage",
                                    CoverageValue = string.Format("{0}", data.Field<string>("FIREDAMAGE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Fire Damage",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("MEDICALEXPENSE") != null)
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Medical Expense",
                                    CoverageValue = string.Format("{0}", data.Field<string>("MEDICALEXPENSE")),
                                    CoverageType = ""
                                });
                            }
                            else
                            {
                                propertyCoverages.Add(new Coverages
                                {
                                    CoverageName = "Medical Expense",
                                    CoverageValue = "0",
                                    CoverageType = ""
                                });
                            }

                        });
                        if (propertyCoverages != null && propertyCoverages.Count > 0)
                        {
                            submissionData.GeneralLiablityCoverages = propertyCoverages;
                        }
                        else //if (DS.Tables[0].AsEnumerable().ToList().Count == 0)
                        {
                            submissionData.GeneralLiablityCoverages = new List<Coverages>
                            {
                                new Coverages { CoverageName = "Each Occurrence", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Aggregate", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Products-Completed Operations Aggregate", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Personal and Advertising Injury", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Fire Damage", CoverageValue = "0", CoverageType = "" },
                                new Coverages { CoverageName = "Medical Expense", CoverageValue = "0", CoverageType = "" }
                            };

                        }
                    }
                    break;
                case "sub_losses_gl":
                    {
                        DS = Database.Sub_Summary_GL_Losses(clientId, email, subGuid);
                        submissionData.GeneralLiablityLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                        //submissionData.GeneralLiablityLosses.Add(new SubmissionLosses { Year = "2020", GrossAmount = "100", NoOfOpenClaims = "10", TotalNoOfClaims = "110" });
                        //submissionData.GeneralLiablityLosses.Add(new SubmissionLosses { Year = "2021", GrossAmount = "200", NoOfOpenClaims = "20", TotalNoOfClaims = "120" });
                        //submissionData.GeneralLiablityLosses.Add(new SubmissionLosses { Year = "2022", GrossAmount = "300", NoOfOpenClaims = "30", TotalNoOfClaims = "130" });
                        //submissionData.GeneralLiablityLosses.Add(new SubmissionLosses { Year = "2023", GrossAmount = "400", NoOfOpenClaims = "40", TotalNoOfClaims = "140" });
                    }
                    break;
                case "sub_exposure_umbrella":
                    {
                        DS = Database.Sub_Summary_Umbrella_Exposure(clientId, email, subGuid);

                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new Exposure
                                    {
                                        Total = string.Format("{0}", dataRow.Field<string>("PAYROLL")) == "" ? "0" : string.Format("{0}", dataRow.Field<string>("PAYROLL")),
                                        ExposureValue1 = string.Format("{0}", dataRow.Field<string>("ANNUALGROSSSALES")),
                                        ExposureValue2 = string.Format("{0}", dataRow.Field<string>("FOREIGNGROSSSALES")),
                                        ExposureValue3 = string.Format("{0}", dataRow.Field<string>("COUNTOFEMPLOYEE")),
                                    });
                        if (data != null)
                        {
                            submissionData.UmbrellaExposure = data.ToList();
                        }
                    }
                    break;
                case "sub_coverage_umbrella":
                    {
                        DS = Database.Sub_Summary_Umbrella_Coverages(clientId, email, subGuid);
                        List<Coverages> propertyCoverages = new List<Coverages>();

                        string coverages = "";
                        string carriers = "";
                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();

                            Coverages cov = new Coverages();
                            if (data.Field<string>("UNDERLYINGCOVERAGES") != null)
                                coverages += string.Format("{0}", data.Field<string>("UNDERLYINGCOVERAGES")) +", ";
                            if (data.Field<string>("CARRIERNAME") != null)
                                carriers += string.Format("{0}", data.Field<string>("CARRIERNAME")) + ", ";
                        });

                        propertyCoverages.Add(new Coverages (){ CoverageName = "Underlying Coverages", CoverageValue = coverages.TrimEnd(' ').TrimEnd(','), CoverageType = "" });
                        propertyCoverages.Add(new Coverages() { CoverageName = "Carrier Name", CoverageValue = carriers.TrimEnd(' ').TrimEnd(','), CoverageType = "" });

                        if (propertyCoverages != null && propertyCoverages.Count > 0)
                        {
                            submissionData.UmbrellaCoverages = propertyCoverages;
                        }
                        else //if (DS.Tables[0].AsEnumerable().ToList().Count == 0)
                        {
                            submissionData.UmbrellaCoverages = new List<Coverages>
                            {
                                new Coverages { CoverageName = "Underlying Coverages", CoverageValue = "", CoverageType = "" },
                                new Coverages { CoverageName = "Carrier Name", CoverageValue = "0", CoverageType = "" }
                            };

                        }
                    }
                    break;
                case "sub_losses_umbrella":
                    {
                        DS = Database.Sub_Summary_Umbrella_Losses(clientId, email, subGuid);
                        submissionData.UmbrellaLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;

            }
            return submissionData;
        }

        
    }
}
