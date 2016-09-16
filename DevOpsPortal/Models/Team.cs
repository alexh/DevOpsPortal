using DevOpsPortal.Models;
using System.Data.Entity;

namespace DevOpsPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MasterTeamInventory")]
    public partial class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Team_ID { get; set; }

        [Column("Team")]
        [StringLength(20)]
        public string TeamFunction { get; set; }

        [StringLength(20)]
        public string TeamName { get; set; }

        public int? AgileLead { get; set; }

        public int? ProductManager { get; set; }

        public int? OperationReadiness { get; set; }

        public int? Architect { get; set; }

        [Column("BA")]
        public int? BA { get; set; }

        [StringLength(15)]
        public string QA { get; set; }

        public int? TeamLead { get; set; }

        [StringLength(150)]
        public string Development { get; set; }

        [StringLength(50)]
        public string EmailDistibution { get; set; }

        [StringLength(10)]
        public string Notes { get; set; }

        public string StandupTime { get; set; }

        [StringLength(15)]
        public string StandupDays { get; set; }
    }
}

public partial class TeamDbContext : DbContext
{
    public TeamDbContext()
        : base("name=Team")
    {
    }

    public virtual DbSet<Team> MasterTeamInventories { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .Property(e => e.TeamFunction)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.TeamName)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.QA)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.Development)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.EmailDistibution)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.Notes)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.StandupTime)
            .IsFixedLength();

        modelBuilder.Entity<Team>()
            .Property(e => e.StandupDays)
            .IsFixedLength();
    }
}