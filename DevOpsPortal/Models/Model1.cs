namespace DevOpsPortal.Models
{
    using System.Data.Entity;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<DeploymentHistory> DeploymentHistories { get; set; }
        public virtual DbSet<MasterProgramInventory> MasterProgramInventories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.Environment)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.Program)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.Machine)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.BaseFolder)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.Instances)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.DeploymentServer)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.ServiceScheduled)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.Team)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.TeamName)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.JenkinsName)
                .IsUnicode(false);

            modelBuilder.Entity<MasterProgramInventory>()
                .Property(e => e.OctopusName)
                .IsUnicode(false);
        }
    }
}