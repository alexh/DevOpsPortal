namespace DevOpsPortal.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DeploymentForJSON
    {
        [Key]
        [StringLength(50)]
        public string DeploymentId { get; set; }

        [Required]
        [StringLength(200)]
        public string DeploymentName { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(210)]
        public string ProjectSlug { get; set; }

        [Required]
        [StringLength(50)]
        public string EnvironmentId { get; set; }

        [Required]
        [StringLength(200)]
        public string EnvironmentName { get; set; }

        [Required]
        [StringLength(150)]
        public string ReleaseId { get; set; }

        [Required]
        [StringLength(100)]
        public string ReleaseVersion { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskId { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskState { get; set; }

        public string Created { get; set; }

        public string QueueTime { get; set; }

        public string StartTime { get; set; }

        public string CompletedTime { get; set; }

        public int? DurationSeconds { get; set; }

        [StringLength(200)]
        public string DeployedBy { get; set; }
    }
}