namespace DevOpsPortal.Models
{
    public class DeploymentStatusDataForJSON
    {
        public string ProjectName { get; set; }

        public string DevReleaseDate { get; set; }

        public string DevRelease { get; set; }

        public string QAReleaseDate { get; set; }

        public string QARelease { get; set; }

        public string QTSReleaseDate { get; set; }

        public string QTSRelease { get; set; }

        public string BetaReleaseDate { get; set; }

        public string BetaRelease { get; set; }

        public string ProductionReleaseDate { get; set; }

        public string ProductionRelease { get; set; }
    }
}