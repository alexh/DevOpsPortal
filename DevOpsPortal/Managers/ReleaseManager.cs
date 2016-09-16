using DevOpsPortal.Models;
using System.Linq;

namespace DevOpsPortal.Managers
{
    public static class ReleaseManager
    {
        private static ReleaseDbContext releaseDbContext = new ReleaseDbContext();

        public static string GetReleaseId(string version, string project)
        {
            string projectId = OctopusManager.Repository.Projects.FindByName(project).Id;

            var release = (from r in releaseDbContext.Release
                           where (r.ProjectId == projectId && r.Version == version)
                           orderby r.Version
                           select r).AsEnumerable().LastOrDefault();

            return release.Id;
        }

        public static string GetLatestProductionReleaseId(string project)
        {
            var latestDeployment = DeploymentsManager.GetLatestDeployment(project, "Production");
            if (latestDeployment == null)
            {
                latestDeployment = new DeploymentForJSON();
                latestDeployment.ReleaseId = "0";
            }

            return latestDeployment.ReleaseId;
        }
    }
}