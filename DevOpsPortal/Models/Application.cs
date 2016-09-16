using DevOpsPortal.Models;
using System.Data.Entity;

namespace DevOpsPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MasterProgramInventory")]
    public partial class Application
    {
        [Key]
        public int App_ID { get; set; }

        [StringLength(50)]
        public string Environment { get; set; }

        [StringLength(50)]
        public string Program { get; set; }

        [StringLength(50)]
        public string Machine { get; set; }

        [StringLength(50)]
        public string BaseFolder { get; set; }

        [StringLength(50)]
        public string Instances { get; set; }

        [StringLength(50)]
        public string DeploymentServer { get; set; }

        [StringLength(50)]
        public string ServiceScheduled { get; set; }

        [StringLength(50)]
        public string Team { get; set; }

        [StringLength(50)]
        public string TeamName { get; set; }

        [StringLength(150)]
        public string Comments { get; set; }

        public bool? IsAutomated { get; set; }

        public bool? JenkinsBuild { get; set; }

        public bool? OctopusDeploy { get; set; }

        public bool? ConfigStructure { get; set; }

        public bool? SolutionStructure { get; set; }

        [StringLength(150)]
        public string JenkinsName { get; set; }

        [StringLength(150)]
        public string OctopusName { get; set; }

        public int? ApplicationTypeId { get; set; }

        public bool Sunsetted { get; set; }
    }
}

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
        : base("name=Application")
    {
    }

    public virtual DbSet<Application> MasterProgramInventories { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
         .Property(e => e.App_ID);

        modelBuilder.Entity<Application>()
            .Property(e => e.Environment)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.Program)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.Machine)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.BaseFolder)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.Instances)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.DeploymentServer)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.ServiceScheduled)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.Team)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.TeamName)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.Comments)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.JenkinsName)
            .IsUnicode(false);

        modelBuilder.Entity<Application>()
            .Property(e => e.OctopusName)
            .IsUnicode(false);
    }
}