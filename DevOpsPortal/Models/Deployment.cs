namespace DevOpsPortal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeploymentHistory")]
    public partial class Deployment
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

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset QueueTime { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? CompletedTime { get; set; }

        public int? DurationSeconds { get; set; }

        [StringLength(200)]
        public string DeployedBy { get; set; }
    }
}

namespace DevOpsPortal.Models
{
    using System.Data.Entity;

    public partial class DeploymentDbContext : DbContext
    {
        public DeploymentDbContext()
            : base("name=OctopusDeployment")
        {
        }

        public virtual DbSet<Deployment> Deployment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}