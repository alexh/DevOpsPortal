using System;

namespace DevOpsPortal.Models
{
    public class DeploymentWithTeamForJSON
    {
        public DeploymentWithTeamForJSON(DeploymentWithTeam d)
        {
            this.DeployedBy = d.DeployedBy;
            this.DeploymentId = d.DeploymentId;
            this.DeploymentName = d.DeploymentName;
            this.DurationSeconds = d.DurationSeconds;
            this.EnvironmentId = d.EnvironmentId;
            this.EnvironmentName = d.EnvironmentName;
            this.ProjectId = d.ProjectId;
            this.ProjectName = d.ProjectName;
            this.ProjectSlug = d.ProjectSlug;
            this.ReleaseId = d.ReleaseId;
            this.ReleaseVersion = d.ReleaseVersion;
            this.TaskId = d.TaskId;
            this.TaskState = d.TaskState;
            this.Team = d.Team;

            TimeSpan offSet = new TimeSpan(-5, 0, 0);

            Created = d.Created.DateTime.ToString();
            QueueTime = d.QueueTime.DateTime.Add(offSet).ToString();
            StartTime = d.StartTime.GetValueOrDefault(new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan())).DateTime.Add(offSet).ToString();
            CompletedTime = d.CompletedTime.GetValueOrDefault(new DateTimeOffset(2000, 1, 1, 0, 0, 0, new TimeSpan())).DateTime.Add(offSet).ToString();
        }

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

        public string Created { get; set; }

        public string QueueTime { get; set; }

        public string StartTime { get; set; }

        public string CompletedTime { get; set; }

        public int? DurationSeconds { get; set; }
        public string DeployedBy { get; set; }

        public string Team { get; set; }
    }
}