using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOpsPortal.Models
{
    public class DeploymentEnvironmentInfo
    {
        public string EnvironmentName { get; set; }
        public string ProjectName { get; set; }
        public int Count { get; set; }
    }
}