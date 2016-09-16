using DevOpsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevOpsPortal.Managers
{
    public static class DeploymentChartStatsManager
    {
        public static DeploymentChartsStats GetDeploymentChartStats()
        {
            DeploymentChartsStats stats = new DeploymentChartsStats();
            var deploy = DeploymentsManager.GetDeployments();

            //int TotalCount = deploy.Count();
            //int SuccessCount = deploy.Where(dep => dep.TaskState == "Success").Count();
            //int FailedCount = deploy.Where(dep => dep.TaskState == "Failed").Count();
            //int TimedOutCount = deploy.Where(dep => dep.TaskState == "TimedOut").Count();
            //int CanceledCount = deploy.Where(dep => dep.TaskState == "Canceled").Count();

            //stats.TotalCount = TotalCount;
            //stats.SuccessCount = SuccessCount;
            //stats.FailedCount = FailedCount;
            //stats.TimedOutCount = TimedOutCount;
            //stats.CanceledCount = CanceledCount;

            DateTime DaysAgo7 = DateTime.Now.AddDays(-7);
            DateTime DaysAgo14 = DateTime.Now.AddDays(-14);
            var TotalSuccess7 = deploy.AsEnumerable().Where(d => d.TaskState == "Success").Where(d => Convert.ToDateTime(d.CompletedTime) >= DaysAgo7);
            int TotalSuccessCount7 = TotalSuccess7.Count();
            int Production7 = TotalSuccess7.Where(d => d.EnvironmentName == "Production").Count();
            int QTS7 = TotalSuccess7.Where(d => d.EnvironmentName == "QTS").Count();
            int QA7 = TotalSuccess7.Where(d => d.EnvironmentName == "QA").Count();
            int Dev7 = TotalSuccess7.Where(d => d.EnvironmentName == "Dev").Count();
            int Beta7 = TotalSuccess7.Where(d => d.EnvironmentName == "Beta").Count();

            var TotalSuccess14 = deploy.AsEnumerable().Where(d => d.TaskState == "Success").Where(d => Convert.ToDateTime(d.CompletedTime) >= DaysAgo14);
            int TotalSuccessCount14 = TotalSuccess14.Count();
            int Production14 = TotalSuccess14.Where(d => d.EnvironmentName == "Production").Count();
            int QTS14 = TotalSuccess14.Where(d => d.EnvironmentName == "QTS").Count();
            int QA14 = TotalSuccess14.Where(d => d.EnvironmentName == "QA").Count();
            int Dev14 = TotalSuccess14.Where(d => d.EnvironmentName == "Dev").Count();
            int Beta14 = TotalSuccess14.Where(d => d.EnvironmentName == "Beta").Count();

            stats.TotalSuccessCount7 = TotalSuccessCount7;
            stats.ProductionCount7 = Production7;
            stats.QTSCount7 = QTS7;
            stats.QACount7 = QA7;
            stats.DevCount7 = Dev7;
            stats.BetaCount7 = Beta7;
            stats.TotalSuccessCount14 = TotalSuccessCount14;
            stats.ProductionCount14 = Production14;
            stats.QTSCount14 = QTS14;
            stats.QACount14 = QA14;
            stats.DevCount14 = Dev14;
            stats.BetaCount14 = Beta14;

            //Line Graph Days

            DateTime Midnight0 = DateTime.Now.Date;
            DateTime Midnight1 = DateTime.Now.AddDays(-1).Date;
            DateTime Midnight2 = DateTime.Now.AddDays(-2).Date;
            DateTime Midnight3 = DateTime.Now.AddDays(-3).Date;
            DateTime Midnight4 = DateTime.Now.AddDays(-4).Date;
            DateTime Midnight5 = DateTime.Now.AddDays(-5).Date;
            DateTime Midnight6 = DateTime.Now.AddDays(-6).Date;
            DateTime Midnight7 = DateTime.Now.AddDays(-7).Date;
            var Successful = deploy.AsEnumerable().Where(d => d.TaskState == "Success");
            var ProductionQ = Successful.Where(d => d.EnvironmentName == "Production");
            var DevQ = Successful.Where(d => d.EnvironmentName == "Dev");
            var QAQ = Successful.Where(d => d.EnvironmentName == "QA");
            var QTSQ = Successful.Where(d => d.EnvironmentName == "QTS");
            var BetaQ = Successful.Where(d => d.EnvironmentName == "Beta");

            string ProductionLineData = "";
            string DevLineData = "";
            string QALineData = "";
            string QTSLineData = "";
            string BetaLineData = "";

            for (int i = 0; i <= 6; i++)
            {
                int prodCurr = ProductionQ.Where(d => Convert.ToDateTime(d.CompletedTime) >= DateTime.Now.AddDays(0 - i).Date && Convert.ToDateTime(d.CompletedTime) < DateTime.Now.AddDays(1 - i).Date).Count();
                ProductionLineData = prodCurr.ToString() + "," + ProductionLineData;
                int QACurr = QAQ.Where(d => Convert.ToDateTime(d.CompletedTime) >= DateTime.Now.AddDays(0 - i).Date && Convert.ToDateTime(d.CompletedTime) < DateTime.Now.AddDays(1 - i).Date).Count();
                QALineData = QACurr.ToString() + "," + QALineData;
                int QTSCurr = QTSQ.Where(d => Convert.ToDateTime(d.CompletedTime) >= DateTime.Now.AddDays(0 - i).Date && Convert.ToDateTime(d.CompletedTime) < DateTime.Now.AddDays(1 - i).Date).Count();
                QTSLineData = QTSCurr.ToString() + "," + QTSLineData;
                int DevCurr = DevQ.Where(d => Convert.ToDateTime(d.CompletedTime) >= DateTime.Now.AddDays(0 - i).Date && Convert.ToDateTime(d.CompletedTime) < DateTime.Now.AddDays(1 - i).Date).Count();
                DevLineData = DevCurr.ToString() + "," + DevLineData;
                int BetaCurr = BetaQ.Where(d => Convert.ToDateTime(d.CompletedTime) >= DateTime.Now.AddDays(0 - i).Date && Convert.ToDateTime(d.CompletedTime) < DateTime.Now.AddDays(1 - i).Date).Count();
                BetaLineData = BetaCurr.ToString() + "," + BetaLineData;
            }

            stats.DevLineData = DevLineData.TrimEnd(',');
            stats.QALineData = QALineData.TrimEnd(',');
            stats.QTSLineData = QTSLineData.TrimEnd(',');
            stats.BetaLineData = BetaLineData.TrimEnd(',');
            stats.ProductionLineData = ProductionLineData.TrimEnd(',');

            //End Days

            //stats.ProductionTime = (float) deploy.Where(dep => dep.EnvironmentName == "Production").Average(dep => dep.DurationSeconds);
            //stats.QTSTime = (float) deploy.Where(dep => dep.EnvironmentName == "QTS").Average(dep => dep.DurationSeconds);
            //stats.QATime = (float) deploy.Where(dep => dep.EnvironmentName == "QA").Average(dep => dep.DurationSeconds);
            //stats.BetaTime = (float) deploy.Where(dep => dep.EnvironmentName == "Beta").Average(dep => dep.DurationSeconds);

            var counting = DeploymentsManager.GetDeploymentList();
            var deployList = counting.Where(dep => dep.EnvironmentName == "Production" || dep.EnvironmentName == "QA" || dep.EnvironmentName == "QTS" || dep.EnvironmentName == "Beta").ToList();
            string Names = "";
            string Production = "";
            string QTS = "";
            string QA = "";
            string Beta = "";
            Dictionary<string, ProgramDeployment> dict = new Dictionary<string, ProgramDeployment>();
            foreach (var d in deployList)
            {
                if (!dict.ContainsKey(d.ProjectName))
                {
                    dict.Add(d.ProjectName, new ProgramDeployment());
                }
                var current = dict[d.ProjectName];
                switch (d.EnvironmentName)
                {
                    case "Production":
                        current.Production = d.Count;
                        break;

                    case "QTS":
                        current.QTS = d.Count;
                        break;

                    case "QA":
                        current.QA = d.Count;
                        break;

                    case "Beta":
                        current.Beta = d.Count;
                        break;
                }
            }
            List<string> NameList = new List<string>();
            foreach (var p in dict)
            {
                NameList.Add(p.Key);
                Names = Names + p.Key.Replace(' ', '-') + ",";
                Production = Production + p.Value.Production + ",";
                QA = QA + p.Value.QA + ",";
                QTS = QTS + p.Value.QTS + ",";
                Beta = Beta + p.Value.Beta + ",";
            }
            Names.TrimEnd(',');
            stats.NamesString = Names;
            stats.ProductionString = Production;
            stats.QTSString = QTS;
            stats.QAString = QA;
            stats.BetaString = Beta;

            //Deployer Stats
            int burch = deploy.Where(d => d.DeployedBy == "jburch").Count();
            int swank = deploy.Where(d => d.DeployedBy == "joshuaswank").Count();
            int tyler = deploy.Where(d => d.DeployedBy == "tshrewsbury").Count();
            int octouser = deploy.Where(d => d.DeployedBy == "octouser").Count();

            int[] rolls = DeploymentsManager.GetRollBackData().ToArray();
            stats.RollBackData = rolls;

            return stats;
        }
    }
}