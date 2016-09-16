namespace DevOpsPortal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeploymentHistory")]
    public partial class DeploymentHistory
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string DeploymentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string DeploymentName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string ProjectId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string ProjectName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(210)]
        public string ProjectSlug { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string EnvironmentId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(200)]
        public string EnvironmentName { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(150)]
        public string ReleaseId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string ReleaseVersion { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string TaskId { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string TaskState { get; set; }

        [Key]
        [Column(Order = 11)]
        public DateTimeOffset Created { get; set; }

        [Key]
        [Column(Order = 12)]
        public DateTimeOffset QueueTime { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? CompletedTime { get; set; }

        public int? DurationSeconds { get; set; }

        [StringLength(200)]
        public string DeployedBy { get; set; }
    }
}