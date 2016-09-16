namespace DevOpsPortal.Models {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CodeDeployRequest {
        public CodeDeployRequest() {
        }

        public CodeDeployRequest(CodeDeployRequestViewModel model) {
            this.CodeDeployRequestID = model.CodeDeployRequestID;
            this.DBDeployRequestID = model.DBDeployRequestID;
            this.User = model.User;
            this.Team = model.Team;
            this.Environment = model.Environment;
            this.AppName = model.AppName;
            this.DeployType = model.DeployType;
            this.JenkinsBuild = model.JenkinsBuild;
            this.DINumber = model.DINumber;
            this.TFSWorkItems = model.TFSWorkItems;
            this.DeployDateTime = model.DeployDateTime;
            this.TestableInQA = model.TestableInQABool;
            this.QAReason = model.QAReason;
            this.QADataImpact = model.QADataImpact;
            this.QAFailureImpact = model.QAFailureImpact;
            this.QAFailureResolution = model.QAFailureResolution;
            this.QAIdentifyFailure = model.QAIdentifyFailure;
            this.QAMitigation = model.QAMitigation;
            this.QATester = model.QATester;
            this.SpecialNotes = model.SpecialNotes;
            this.ConfirmationEmail = model.ConfirmationEmail;
            this.SubmissionTime = model.SubmissionTime;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Don't display?
        public int CodeDeployRequestID { get; set; }

        //Don't display?
        public int? DBDeployRequestID { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please select a team.")]
        [StringLength(50)]
        public string Team { get; set; }

        [StringLength(50)]
        public string Environment { get; set; }

        [StringLength(50)]
        public string AppName { get; set; }

        //Don't display
        [StringLength(50)]
        public string DeployType { get; set; }

        public Int16? JenkinsBuild { get; set; }

        public Int16? DINumber { get; set; }

        [StringLength(50)]
        public string TFSWorkItems { get; set; }

        public DateTime? DeployDateTime { get; set; }

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

        [StringLength(500)]
        public string SpecialNotes { get; set; }

        [StringLength(100)]
        public string ConfirmationEmail { get; set; }

        public DateTime? SubmissionTime { get; set; }
    }
}

namespace DevOpsPortal.Models {
    using System.Data.Entity;

    public partial class CodeDeployRequestDbContext : DbContext {
        public CodeDeployRequestDbContext()
            : base("name=CodeDeploy") {
        }

        public virtual DbSet<CodeDeployRequest> CodeDeployRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        }

        public System.Data.Entity.DbSet<DevOpsPortal.Models.DBDeployRequest> DBDeployRequests { get; set; }
    }
}