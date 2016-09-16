using DevOpsPortal.Models;
using Octopus.Client.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DevOpsPortal.Managers
{
    public static class ApplicationManager
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static Application GetApplication(int id)
        {
            return db.MasterProgramInventories.Find(id);
        }

        public static List<Application> GetApplications()
        {
            List<Application> applications = new List<Application>();

            var projectResources = GetProjectResources();

            foreach (var project in projectResources)
            {
                applications.Add(ConvertProjectResourceToApplication(project));
            }
            return applications;
        }

        public static List<Application> GetApplicationsFromMasterProgramInventory()
        {
            var apps = from m in db.MasterProgramInventories
                       where (!m.Sunsetted && m.Team != "DevOps")
                       orderby m.Program, m.Environment
                       select m;

            return apps.ToList();
        }

        public static List<Application> GetDistinctApplications()
        {
            var apps = db.Database.SqlQuery<Application>("select [App_ID] ,[Environment] ,[Program] ,[Machine] ,[BaseFolder] ,[Instances] ,[DeploymentServer] ,[ServiceScheduled] ,[Team] ,[TeamName] ,[Comments] ,[IsAutomated] ,[JenkinsBuild] ,[OctopusDeploy] ,[ConfigStructure] ,[SolutionStructure] ,[JenkinsName] ,[OctopusName] ,[ApplicationTypeId] ,[Sunsetted] from (select *, row_number() over(partition by Program order by App_ID) as RowNbr from MasterProgramInventory ) source where RowNbr = 1").AsEnumerable();
            return apps.ToList();
        }

        public static List<AppJSON> GetApplicationsForTeam(string team)
        {
            var apps = from m in db.MasterProgramInventories
                       where (!m.Sunsetted && m.Team == team)
                       select m.Program;
            apps = apps.Distinct();
            List<AppJSON> applications = new List<AppJSON>();
            foreach (string a in apps)
            {
                applications.Add(new AppJSON(a));
            }

            return applications;
        }

        private static List<ProjectResource> GetProjectResources()
        {
            List<ProjectResource> projectResources = new List<ProjectResource>();

            projectResources = OctopusManager.Repository.Projects.FindAll();

            return projectResources;
        }

        public static ProjectResource GetProjectResourceByAppName(string applicationName)
        {
            ProjectResource projectResource = new ProjectResource();

            projectResource = OctopusManager.Repository.Projects.FindByName(applicationName);

            return projectResource;
        }

        private static Application ConvertProjectResourceToApplication(ProjectResource projectResource)
        {
            Application application = new Application();
            application.Program = projectResource.Name;

            return application;
        }

        public static void SaveApplication(Application application)
        {
            db.MasterProgramInventories.Add(application);
            db.SaveChanges();
        }

        public static void UpdateApplication(Application application)
        {
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteApplication(int id)
        {
            Application application = GetApplication(id);
            db.MasterProgramInventories.Remove(application);
            db.SaveChanges();
        }
    }
}