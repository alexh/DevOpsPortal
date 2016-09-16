namespace DevOpsPortal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MasterServerInventory")]
    public partial class Server
    {
        [Key]
        public int ServerId { get; set; }

        [Required]
        [StringLength(40)]
        public string ComputerName { get; set; }

        [Required]
        [StringLength(25)]
        public string IPAddress { get; set; }

        [Required]
        [StringLength(70)]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [StringLength(100)]
        public string OS { get; set; }

        [Required]
        [StringLength(50)]
        public string TotalMemory { get; set; }

        public int? CPUSockets { get; set; }

        public int? CPUCoresPerSocket { get; set; }

        [Required]
        [StringLength(300)]
        public string Drives { get; set; }

        [Required]
        [StringLength(50)]
        public string ProbeSiteName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateImported { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }
    }
}

namespace DevOpsPortal.Models
{
    using System.Data.Entity;

    public partial class ServerDBContext : DbContext
    {
        public ServerDBContext()
            : base("name=Server")
        {
        }

        public virtual DbSet<Server> MasterServerInventories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>()
                .Property(e => e.ComputerName)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.Make)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.OS)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.TotalMemory)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.Drives)
                .IsUnicode(false);

            modelBuilder.Entity<Server>()
                .Property(e => e.ProbeSiteName)
                .IsUnicode(false);
        }
    }
}