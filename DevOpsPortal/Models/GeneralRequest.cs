using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.IO;
using DevOpsPortal.Models;
using System.Data.Entity;

namespace DevOpsPortal.Models {
    public partial class GeneralRequest {

        public GeneralRequest() {

        }

        public GeneralRequest(GeneralRequestViewModel model, byte[] Image) {
            this.GeneralRequestID = model.GeneralRequestID;
            this.Notes = model.Notes;
            this.User = model.User;
            this.Team = model.Team;
            this.Attachment = Image;
        }

        public int GeneralRequestID { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        [StringLength(50)]
        public string Team { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public byte[] Attachment { get; set; }
    }


    public partial class GeneralRequestDbContext : DbContext {
        public GeneralRequestDbContext()
            : base("name=CodeDeploy") {
        }

        public virtual DbSet<GeneralRequest> GeneralRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<GeneralRequest>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralRequest>()
                .Property(e => e.Team)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralRequest>()
                .Property(e => e.Notes)
                .IsUnicode(false);
        }
    }
}
