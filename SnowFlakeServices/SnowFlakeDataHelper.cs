﻿using Microsoft.Extensions.Configuration;
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
            List<DashboardGraph> lstDasboardgraph = new List<DashboardGraph>();
            DataSet DS=new DataSet();
            switch (Type.ToLower())
            {
                case "countbyturnaroundtime":
                    {
                        DS = Database.DashboardGraph_CountByTurnaroundTime(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DashboardGraph
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("TAT")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID"))
                                    }).ToList();
                        List<DashboardGraph> dashboard = new List<DashboardGraph>();
                        foreach (var row in lstDasboardgraph)
                        {
                            DashboardGraph graph = new DashboardGraph
                            {
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
                        DS = Database.DashboardGraph_CountByLOB(dashboardFilter.TopNumber,dashboardFilter.CLIENTID,dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DashboardGraph
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
                                   .Select(dataRow => new DashboardGraph
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
                                   .Select(dataRow => new DashboardGraph
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
                                   .Select(dataRow => new DashboardGraph
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
                                   .Select(dataRow => new DashboardGraph
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("NAICCODE")),
                                       Measure = string.Format("{0}", dataRow.Field<Int64>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;

            }
            return lstDasboardgraph;
        }
    }
}
