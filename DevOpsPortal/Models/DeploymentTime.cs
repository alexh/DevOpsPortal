namespace DevOpsPortal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public partial class DeploymentTime
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string DeploymentTimeName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string TimeOfDay { get; set; }

        [Key]
        [Column(Order = 3)]
        public TimeSpan Time { get; set; }
    }

    public partial class DeploymentTimetDbContext : DbContext
    {
        public DeploymentTimetDbContext()
            : base("name=Application")
        {
        }

        public virtual DbSet<DeploymentTime> DeploymentTime { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}