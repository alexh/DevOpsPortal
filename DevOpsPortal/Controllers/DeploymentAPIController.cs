using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System.Web.Http;

namespace DevOpsPortal.Controllers
{
    public class DeploymentAPIController : ApiController
    {
        // GET: api/DeploymentAPI
        [HttpGet]
        public DeploymentForJSON Get(string applicationName, string environment)
        {
            return DeploymentsManager.GetLatestDeployment(applicationName, environment);
        }

        public string Get()
        {
            return "test";
        }

        // GET: api/DeploymentAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DeploymentAPI

        // PUT: api/DeploymentAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeploymentAPI/5
        public void Delete(int id)
        {
        }
    }
}