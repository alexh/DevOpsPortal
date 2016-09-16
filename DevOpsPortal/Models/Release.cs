namespace DevOpsPortal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Release")]
    public partial class Release
    {
        [StringLength(150)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Version { get; set; }

        public DateTimeOffset Assembled { get; set; }

        [Required]
        [StringLength(150)]
        public string ProjectId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProjectVariableSetSnapshotId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProjectDeploymentProcessSnapshotId { get; set; }

        [Required]
        public string JSON { get; set; }

        [Required]
        [StringLength(50)]
        public string ChannelId { get; set; }
    }

    public partial class ReleaseDbContext : DbContext
    {
        public ReleaseDbContext()
            : base("name=OctopusDeployment")
        {
        }

        public virtual DbSet<Release> Release { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}