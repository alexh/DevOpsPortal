namespace DevOpsPortal.Models {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DBDeployRequest {

        public DBDeployRequest() {

        }
        public DBDeployRequest(DBDeployRequestViewModel model) {
            this.CodeDeployRequestID = model.CodeDeployRequestID;
            this.User = model.User;
            this.Team = model.Team;
            this.Environment = model.Environment;

            this.Changesets = model.Changesets;
            this.CodeLocation = model.CodeLocation;
            this.DatabaseName = model.DatabaseName;
            this.DataScriptNames = model.DataScriptNames;

            this.TestableInQA = model.TestableInQABool;
            this.QAReason = model.QAReason;
            this.QADataImpact = model.QADataImpact;
            this.QAFailureImpact = model.QAFailureImpact;
            this.QAFailureResolution = model.QAFailureResolution;
            this.QAIdentifyFailure = model.QAIdentifyFailure;
            this.QAMitigation = model.QAMitigation;
            this.QATester = model.QATester;

            this.TestableInQTS = model.TestableInQTSBool;
            this.QTSReason = model.QTSReason;
            this.QTSDataImpact = model.QTSDataImpact;
            this.QTSFailureImpact = model.QTSFailureImpact;
            this.QTSFailureResolution = model.QTSFailureResolution;
            this.QTSIdentifyFailure = model.QTSIdentifyFailure;
            this.QTSMitigation = model.QTSMitigation;

            this.TFSWorkItems = model.TFSWorkItems;
            this.DeploymentCycle = model.DeploymentCycle;

            this.SubmissionTime = model.SubmissionTime;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DBDeployRequestID { get; set; }

        public int? CodeDeployRequestID { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        [StringLength(50)]
        public string Team { get; set; }

        [StringLength(50)]
        public string Environment { get; set; }

        [StringLength(50)]
        public string DatabaseName { get; set; }

        [StringLength(50)]
        public string CodeLocation { get; set; }

        [StringLength(50)]
        public string Changesets { get; set; }

        [StringLength(50)]
        public string TFSWorkItems { get; set; }

        [StringLength(50)]
        public string DataScriptNames { get; set; }

        [StringLength(20)]
        public string DeploymentCycle { get; set; }

        public bool? TestableInQA { get; set; }

        [StringLength(500)]
        public string QAReason { get; set; }

        [StringLength(500)]
        public string QAMitigation { get; set; }

        [StringLength(500)]
        public string QAIdentifyFailure { get; set; }

        [StringLength(500)]
        public string QAFailureImpact { get; set; }

        [StringLength(500)]
        public string QADataImpact { get; set; }

        [StringLength(500)]
        public string QAFailureResolution { get; set; }

        [StringLength(50)]
        public string QATester { get; set; }


        public bool? TestableInQTS { get; set; }

        [StringLength(500)]
        public string QTSReason { get; set; }

        [StringLength(500)]
        public string QTSMitigation { get; set; }

        [StringLength(500)]
        public string QTSIdentifyFailure { get; set; }

        [StringLength(500)]
        public string QTSFailureImpact { get; set; }

        [StringLength(500)]
        public string QTSDataImpact { get; set; }

        [StringLength(500)]
        public string QTSFailureResolution { get; set; }

        public DateTime? SubmissionTime { get; set; }
    }
}

namespace DevOpsPortal.Models {
    using System.Data.Entity;

    public partial class DBDeployDbContext : DbContext {
        public DBDeployDbContext()
            : base("name=DBDeploy") {
        }

        public virtual DbSet<DBDeployRequest> DBDeployRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        }
    }
}