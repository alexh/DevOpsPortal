using System;

namespace DevOpsPortal.Models
{
    public class DeploymentStatusData
    {
        public string ProjectName { get; set; }

        public DateTimeOffset DevReleaseDate { get; set; }

        public string DevRelease { get; set; }

        public DateTimeOffset QAReleaseDate { get; set; }

        public string QARelease { get; set; }

        public DateTimeOffset QTSReleaseDate { get; set; }

        public string QTSRelease { get; set; }

        public DateTimeOffset BetaReleaseDate { get; set; }

        public string BetaRelease { get; set; }

        public DateTimeOffset ProductionReleaseDate { get; set; }

        public string ProductionRelease { get; set; }
    }
}