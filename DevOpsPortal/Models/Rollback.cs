using System;

namespace DevOpsPortal.Models
{
    public class Rollback
    {
        public string DeploymentId { get; set; }

        public string DeploymentName { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectSlug { get; set; }

        public string EnvironmentId { get; set; }

        public string EnvironmentName { get; set; }

        public string ReleaseIdTo { get; set; }

        public string ReleaseIdFrom { get; set; }

        public string ReleaseVersionTo { get; set; }

        public string ReleaseVersionFrom { get; set; }

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