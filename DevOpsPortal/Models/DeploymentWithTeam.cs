using System;

namespace DevOpsPortal.Models
{
    public partial class DeploymentWithTeam
    {
        public string DeploymentId { get; set; }

        public string DeploymentName { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectSlug { get; set; }

        public string EnvironmentId { get; set; }

        public string EnvironmentName { get; set; }

        public string ReleaseId { get; set; }

        public string ReleaseVersion { get; set; }

        public string TaskId { get; set; }

        public string TaskState { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset QueueTime { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? CompletedTime { get; set; }

        public int? DurationSeconds { get; set; }

        public string DeployedBy { get; set; }

        public string Team { get; set; }
    }
}