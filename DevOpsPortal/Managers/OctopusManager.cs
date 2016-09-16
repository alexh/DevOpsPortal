using Octopus.Client;
using System.Configuration;

namespace DevOpsPortal.Managers
{
    public static class OctopusManager
    {
        private static string server = ConfigurationManager.AppSettings["OctopusServerUrl"];
        private static string apiKey = ConfigurationManager.AppSettings["OctopusAPIKey"];
        private static OctopusServerEndpoint endPoint = new OctopusServerEndpoint(server, apiKey);
        public static OctopusRepository Repository = new OctopusRepository(endPoint);
    }
}