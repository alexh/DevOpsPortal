using DevOpsPortal.Models;
using Octopus.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevOpsPortal.Managers {
    public static class DeploymentsManager {
        private static DeploymentTimetDbContext deploymentDBContext = new DeploymentTimetDbContext();
        private static DeploymentDbContext db = new DeploymentDbContext();
        private static Model1 dbContext = new Model1();
        private static List<DeploymentForJSON> deployments = new List<DeploymentForJSON>();
        private static DateTime LastUpdated = DateTime.Now;
        private static TimeSpan offSet = new TimeSpan(-5, 0, 0);


        public static Deployment GetDeployment(string id) {
            return db.Deployment.Find(id);
        }
        /// <summary>
        /// Get's All the Deployments from Octopus Deploy
        /// </summary>
        /// <returns></returns>
        public static List<DeploymentForJSON> GetDeployments() {
            if (deployments.Count == 0 || LastUpdated == DateTime.Now.AddMinutes(-5)) {
                var deploys = (from m in db.Deployment
                               where (m.EnvironmentName != "DevOps")
                               orderby m.CompletedTime descending
                               select m).AsEnumerable().Select(m => new DeploymentForJSON {
                                   DeploymentId = m.DeploymentId,
                                   DeploymentName = m.DeploymentName,
                                   ProjectId = m.ProjectId,
                                   ProjectName = m.ProjectName,
                                   ProjectSlug = m.ProjectSlug,
                                   EnvironmentId = m.EnvironmentId,
                                   EnvironmentName = m.EnvironmentName,
                                   ReleaseId = m.ReleaseId,
                                   ReleaseVersion = m.ReleaseVersion,
                                   TaskId = m.TaskId,
                                   TaskState = m.TaskState,
                                   Created = m.Created.DateTime.ToString(),
                                   QueueTime = m.QueueTime.DateTime.Add(offSet).ToString(),
                                   StartTime = m.StartTime.GetValueOrDefault(new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan())).DateTime.Add(offSet).ToString(),
                                   CompletedTime = m.CompletedTime.GetValueOrDefault(new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan())).DateTime.Add(offSet).ToString(),
                                   DurationSeconds = m.DurationSeconds,
                                   DeployedBy = m.DeployedBy
                               }).ToList();
                deployments = deploys;

                return deploys;
            } else {
                return deployments;
            }
        }

        public static List<DeploymentForJSON> GetDeployments(int range) {
            if (deployments.Count == 0 || LastUpdated == DateTime.Now.AddMinutes(-5)) {
                DateTime limit = DateTime.Today.AddMonths(-1 * range);
                var deploys = (from m in db.Deployment
                               where (m.EnvironmentName != "DevOps")
                               orderby m.CompletedTime descending
                               select m).Where(m => m.Created > limit).Select(m => new DeploymentForJSON {
                                   DeploymentId = m.DeploymentId,
                                   DeploymentName = m.DeploymentName,
                                   ProjectId = m.ProjectId,
                                   ProjectName = m.ProjectName,
                                   ProjectSlug = m.ProjectSlug,
                                   EnvironmentId = m.EnvironmentId,
                                   EnvironmentName = m.EnvironmentName,
                                   ReleaseId = m.ReleaseId,
                                   ReleaseVersion = m.ReleaseVersion,
                                   TaskId = m.TaskId,
                                   TaskState = m.TaskState,
                                   Created = m.Created.DateTime.ToString(),
                                   QueueTime = m.QueueTime.DateTime.Add(offSet).ToString(),
                                   StartTime = m.StartTime.GetValueOrDefault(new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan())).DateTime.Add(offSet).ToString(),
                                   CompletedTime = m.CompletedTime.GetValueOrDefault(new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan())).DateTime.Add(offSet).ToString(),
                                   DurationSeconds = m.DurationSeconds,
                                   DeployedBy = m.DeployedBy
                               }).ToList();
                deployments = deploys;

                return deploys;
            } else {
                return deployments;
            }
        }


        public static List<DeploymentEnvironmentInfo> GetDeploymentList() {
            var counting = from d in db.Deployment
                           group d by new { d.ProjectName, d.EnvironmentName } into g
                           select new DeploymentEnvironmentInfo { ProjectName = g.Key.ProjectName, EnvironmentName = g.Key.EnvironmentName, Count = g.Count() };

            counting = counting.OrderByDescending(a => a.Count);

            return counting.ToList();
        }

        /// <summary>
        /// Gets the latest deployment for the application and environment passed in
        /// from Octopus Deploy
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static DeploymentForJSON GetLatestDeployment(string applicationName, string environment) {
            GetDeployments();

            DeploymentForJSON deploy = (from m in deployments
                                        where (m.EnvironmentName != "DevOps" && m.EnvironmentName == environment && m.ProjectName == applicationName)
                                        orderby m.CompletedTime descending
                                        select m).AsEnumerable().Select(m => new DeploymentForJSON {
                                            DeploymentId = m.DeploymentId,
                                            DeploymentName = m.DeploymentName,
                                            ProjectId = m.ProjectId,
                                            ProjectName = m.ProjectName,
                                            ProjectSlug = m.ProjectSlug,
                                            EnvironmentId = m.EnvironmentId,
                                            EnvironmentName = m.EnvironmentName,
                                            ReleaseId = m.ReleaseId,
                                            ReleaseVersion = m.ReleaseVersion,
                                            TaskId = m.TaskId,
                                            TaskState = m.TaskState,
                                            Created = m.Created,
                                            QueueTime = m.QueueTime,
                                            StartTime = m.StartTime,
                                            CompletedTime = m.CompletedTime,
                                            DurationSeconds = m.DurationSeconds,
                                            DeployedBy = m.DeployedBy
                                        }).FirstOrDefault();

            return deploy;
        }

        public static List<DeploymentStatusDataForJSON> GetDeploymentStatus() {
            //Only has ProjectName, EnvironmentName, ReleaseVersion CompletedTime
            //Finds the most recent of each Project-Environment Pair
            var context = new DeploymentDbContext();
            var dep = context.Database.SqlQuery<DeploymentStatusData>("Select distinct d.ProjectName, (select top 1 q.ReleaseVersion from DeploymentHistory q where q.EnvironmentName = 'Dev' and q.ProjectName = d.ProjectName  and q.TaskState = 'Success'  order by q.[CompletedTime] desc) as DevRelease, IsNull((select top 1 q.[CompletedTime] from DeploymentHistory q where q.EnvironmentName = 'Dev' and q.ProjectName = d.ProjectName and q.TaskState = 'Success'  order by q.[CompletedTime] desc), '2000-01-01') as DevReleaseDate, (select top 1 q.ReleaseVersion from DeploymentHistory q where q.EnvironmentName = 'QA' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc) as QARelease, IsNull((select top 1 q.[CompletedTime] from DeploymentHistory q where q.EnvironmentName = 'QA' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc), '2000-01-01') as QAReleaseDate, (select top 1 q.ReleaseVersion from DeploymentHistory q where q.EnvironmentName = 'QTS' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc) as QTSRelease, IsNull((select top 1 q.[CompletedTime] from DeploymentHistory q where q.EnvironmentName = 'QTS' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc), '2000-01-01') as QTSReleaseDate, (select top 1 q.ReleaseVersion from DeploymentHistory q where q.EnvironmentName = 'Beta' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc) as BetaRelease, IsNull((select top 1 q.[CompletedTime] from DeploymentHistory q where q.EnvironmentName = 'Beta' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc), '2000-01-01') as BetaReleaseDate, (select top 1 q.ReleaseVersion from DeploymentHistory q where q.EnvironmentName = 'Production' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc) as ProductionRelease, IsNull((select top 1 q.[CompletedTime] from DeploymentHistory q where q.EnvironmentName = 'Production' and q.ProjectName = d.ProjectName and q.TaskState = 'Success' order by q.[CompletedTime] desc), '2000-01-01') as ProductionReleaseDate From [DeploymentHistory] As d WHERE d.EnvironmentName != 'DevOps' AND d.TaskState = 'Success'").ToList();

            List<DeploymentStatusDataForJSON> list = new List<DeploymentStatusDataForJSON>();

            foreach (var d in dep) {
                DeploymentStatusDataForJSON current = new DeploymentStatusDataForJSON();
                current.ProjectName = d.ProjectName;
                current.DevRelease = d.DevRelease;

                current.DevReleaseDate = d.DevReleaseDate.DateTime.AddHours(-5).ToString();

                current.QARelease = d.QARelease;

                current.QAReleaseDate = d.QAReleaseDate.DateTime.AddHours(-5).ToString();

                current.QTSRelease = d.QTSRelease;

                current.QTSReleaseDate = d.QTSReleaseDate.DateTime.AddHours(-5).ToString();

                current.BetaRelease = d.BetaRelease;

                current.BetaReleaseDate = d.BetaReleaseDate.DateTime.AddHours(-5).ToString();

                current.ProductionRelease = d.ProductionRelease;

                current.ProductionReleaseDate = d.ProductionReleaseDate.DateTime.AddHours(-5).ToString();

                list.Add(current);
            }

            return list;
        }

        public static List<int> GetRollBackData() {
            List<RollbackData> list = new List<RollbackData>();
            List<int> values = new List<int>();
            ApplicationDbContext context = new ApplicationDbContext();
            var d = context.Database.SqlQuery<Rollback>("SELECT DISTINCT ProjectName ,ReleaseVersionTo ,ReleaseVersionFrom ,EnvironmentName ,Created ,Team FROM Rollbacks as r LEFT JOIN MasterProgramInventory as p ON r.ProjectName = p.OctopusName order by Created");
            var rollbacks = d.AsEnumerable();
            string[] teams = new string[] { "API", "BIAndReporting", "Communications", "Customer", "Engagement", "Integration", "InternalSolutions", "Shared", "Vehicle", "VehicleAndFinance", "VinLens", "Websites", "Workflow" };
            foreach (string team in teams) {
                RollbackData current = new RollbackData();
                current.TeamName = team;
                current.Count = d.Where(roll => roll.Team == team).Count();
                list.Add(current);
                values.Add(current.Count);
            }

            return values;
        }

        public static void Deploy(string applicationName, string releaseId, string environment, DateTime? date, bool isNightTime = false) {
            DeploymentResource deploymentResource = new DeploymentResource();
            deploymentResource.UseGuidedFailure = true;
            deploymentResource.ProjectId = ApplicationManager.GetProjectResourceByAppName(applicationName).Id;
            deploymentResource.EnvironmentId = EnvironmentManager.GetEnvironmentId(environment);
            deploymentResource.QueueTime = GetNextDeploymentSlot(date, isNightTime);
            deploymentResource.ReleaseId = releaseId;

            var test = string.Empty;

            OctopusManager.Repository.Deployments.Create(deploymentResource);
        }

        private static DateTimeOffset GetNextDeploymentSlot(DateTime? date, bool isNightTime) {
            DeploymentTime deploymentTime = new DeploymentTime();

            if (!date.HasValue) {
                date = DateTime.Now;
            }

            //Check to see if the current time is in the morning or not
            if (date.Value.ToString("tt") == "AM") {
                date = GetMorningTimeSlot(date.Value);
            } else {
                date = GetAfternoonTimeSlot(date.Value);
            }

            //If the Deployment is flagged for a NightTime deployment then override the deployment slot with the next available Nightime Deployment
            if (isNightTime) {
                date = GetEveningTimeSlot(date.Value);
            }

            return new DateTimeOffset(date.Value);
        }

        private static DateTime GetMorningTimeSlot(DateTime date) {
            //Get the last morning deployment slot
            var lastMorningSlot = (from m in deploymentDBContext.DeploymentTime
                                   where (m.TimeOfDay == "Morning")
                                   orderby m.DeploymentTimeName
                                   select m).AsEnumerable().LastOrDefault();

            //If the requested Date and Time has an available slot during the morning
            if (date.TimeOfDay <= lastMorningSlot.Time) {
                var deploymentTime = (from m in deploymentDBContext.DeploymentTime
                                      where ((m.TimeOfDay == "Morning") && (m.Time <= date.TimeOfDay && m.Time > lastMorningSlot.Time))
                                      orderby m.DeploymentTimeName
                                      select m).AsEnumerable().FirstOrDefault();

                if (deploymentTime == null) {
                    deploymentTime = lastMorningSlot;
                }

                date = SetTimeSlot(date, deploymentTime);
            } else //If the time is after the last morning slot then set the deployment for the first afternoon time slot
              {
                var deploymentTime = deploymentDBContext.DeploymentTime.FirstOrDefault(d => d.TimeOfDay == "Afternoon");
                date = SetTimeSlot(date, deploymentTime);
            }
            return date;
        }

        //Get the Next Afternoon time slot or the next morning depending on if there are any afternoon time slots available
        private static DateTime GetAfternoonTimeSlot(DateTime date) {
            //Get the last afternoon deployment slot
            var lastAfternoonSlot = (from m in deploymentDBContext.DeploymentTime
                                     where (m.TimeOfDay == "Afternoon")
                                     orderby m.DeploymentTimeName
                                     select m).AsEnumerable().LastOrDefault();

            //Check if the Requested Date and Time has an available slot during the afternoon
            if (date.TimeOfDay <= lastAfternoonSlot.Time) {
                var deploymentTime = (from m in deploymentDBContext.DeploymentTime
                                      where (m.TimeOfDay == "Afternoon")
                                      orderby m.DeploymentTimeName
                                      select m).AsEnumerable().FirstOrDefault();
                date = SetTimeSlot(date, deploymentTime);
            } else //If the time is after the last afternoon slot then set the deployment for the next day's first available morning deployment time slot
              {
                var deploymentTime = (from m in deploymentDBContext.DeploymentTime
                                      where (m.TimeOfDay == "Morning")
                                      orderby m.DeploymentTimeName
                                      select m).AsEnumerable().FirstOrDefault();

                date = SetTimeSlot(date.AddDays(1), deploymentTime);
            }

            return date;
        }

        private static DateTime GetEveningTimeSlot(DateTime date) {
            var lastEveningSlot = (from m in deploymentDBContext.DeploymentTime
                                   where (m.TimeOfDay == "Evening")
                                   orderby m.DeploymentTimeName
                                   select m).AsEnumerable().LastOrDefault();

            if (date.TimeOfDay <= lastEveningSlot.Time && date.DayOfWeek == DayOfWeek.Tuesday) {
                var deploymentTime = (from m in deploymentDBContext.DeploymentTime
                                      where ((m.TimeOfDay == "Evening") && (m.Time <= date.TimeOfDay && m.Time > lastEveningSlot.Time))
                                      orderby m.DeploymentTimeName
                                      select m).AsEnumerable().FirstOrDefault();

                date = SetTimeSlot(date, deploymentTime);
            } else {
                var deploymentTime = (from m in deploymentDBContext.DeploymentTime
                                      where ((m.TimeOfDay == "Evening"))
                                      orderby m.DeploymentTimeName
                                      select m).AsEnumerable().FirstOrDefault();

                date = SetTimeSlot(GetNextWeekday(DayOfWeek.Tuesday), deploymentTime);
            }

            return date;
        }

        private static DateTime SetTimeSlot(DateTime date, DeploymentTime deploymentTime) {
            date = date.Date + deploymentTime.Time;
            return date;
        }

        private static DateTime GetNextWeekday(DayOfWeek day) {
            DateTime result = DateTime.Now.AddDays(1);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
    }
}