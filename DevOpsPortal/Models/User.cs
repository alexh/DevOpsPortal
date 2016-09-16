namespace DevOpsPortal.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MasterUserInventory")]
    public partial class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_ID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        public string Team { get; set; }

        [StringLength(50)]
        public string JobTitle { get; set; }

        [StringLength(50)]
        public string BusinessPhone { get; set; }

        [StringLength(50)]
        public string BusinessPhoneExtension { get; set; }

        [StringLength(50)]
        public string HomePhone { get; set; }

        [StringLength(50)]
        public string MobilePhone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string OfficeLocation { get; set; }
    }
}

namespace DevOpsPortal.Models {
    using System.Data.Entity;

    public partial class UserDbContext : DbContext {
        public UserDbContext()
            : base("name=User1") {
        }

        public virtual DbSet<User> MasterUserInventories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Company)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.JobTitle)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.BusinessPhone)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.BusinessPhoneExtension)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.HomePhone)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.MobilePhone)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.OfficeLocation)
                .IsFixedLength();
        }
    }
}