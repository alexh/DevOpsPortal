using DevOpsPortal.Models;
using Octopus.Client.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DevOpsPortal.Managers
{
    public static class EnvironmentManager
    {
        //private static Envi db = new DeploymentDbContext();
        //private List<Environment> environments = new List<Environment>();

        public static string GetEnvironmentId(string environment)
        {
            EnvironmentResource environmentResource = OctopusManager.Repository.Environments.FindByName(environment);

            return environmentResource.Id;
        }

        public static List<SelectListItem> GetEnvironments()
        {
            List<SelectListItem> environments = new List<SelectListItem>();

            environments.Add(new SelectListItem { Text = "QA", Value = "QA" });
            environments.Add(new SelectListItem { Text = "QTS", Value = "QTS" });
            environments.Add(new SelectListItem { Text = "Dev", Value = "Dev" });
            environments.Add(new SelectListItem { Text = "QA-CMS-Stage", Value = "QA-CMS-Stage" });
            environments.Add(new SelectListItem { Text = "QA-CMS-Live", Value = "QA-CMS-Live" });
            environments.Add(new SelectListItem { Text = "QTS-CMS-Stage", Value = "QTS-CMS-Stage" });
            environments.Add(new SelectListItem { Text = "QTS-CMS-Live", Value = "QTS-CMS-Live" });

            return environments;
        }

        public static void RefreshEnvironment(string environment, DateTime date, bool isNightTime = false)
        {
            List<Application> applications = ApplicationManager.GetApplications();

            foreach (var application in applications)
            {
                var latestProductionReleaseId = ReleaseManager.GetLatestProductionReleaseId(application.Program);

                if (latestProductionReleaseId != "0")
                {
                    DeploymentsManager.Deploy(application.Program, latestProductionReleaseId, environment, date, isNightTime);
                }
            }
        }
    }
}