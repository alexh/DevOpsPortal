namespace DevOpsPortal.Models {
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class DBDeployRequestViewModel {
        public DBDeployRequestViewModel() {
        }

        public DBDeployRequestViewModel(FullRequestViewModel model) {
            this.User = model.User;
            this.Team = model.Team;
            this.Environment = model.Environment;

            this.Changesets = model.Changesets;
            this.CodeLocation = model.CodeLocation;
            this.DatabaseName = model.DatabaseName;
            this.DataScriptNames = model.DataScriptNames;
            this.TestableInQABool = model.TestableInQABoolDB;
            this.TestableInQA = model.TestableInQABoolDB;
            this.QAReason = model.QAReasonDB;
            this.QADataImpact = model.QADataImpactDB;
            this.QAFailureImpact = model.QAFailureImpactDB;
            this.QAFailureResolution = model.QAFailureResolutionDB;
            this.QAIdentifyFailure = model.QAIdentifyFailureDB;
            this.QAMitigation = model.QAMitigationDB;
            this.QATester = model.QATesterDB;

            this.TestableInQTS = model.TestableInQTSBoolDB;
            this.QTSReason = model.QTSReasonDB;
            this.QTSDataImpact = model.QTSDataImpactDB;
            this.QTSFailureImpact = model.QTSFailureImpactDB;
            this.QTSFailureResolution = model.QTSFailureResolutionDB;
            this.QTSIdentifyFailure = model.QTSIdentifyFailureDB;
            this.QTSMitigation = model.QTSMitigationDB;

            this.TFSWorkItems = model.TFSWorkItemsDB;
            this.DeploymentCycle = model.DeploymentCycle;

            this.SubmissionTime = model.SubmissionTime;
        }

        //Don't display?
        public int DBDeployRequestID { get; set; }

        //Don't display?
        public int CodeDeployRequestID { get; set; }

        [StringLength(50)]
        [Required]
        public string User { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Please select a team.")]
        [StringLength(50)]
        [Required]
        public string Team { get; set; }

        [Required]
        [StringLength(50)]
        public string Environment { get; set; }

        [StringLength(50)]
        public string DatabaseName { get; set; }

        [StringLength(50)]
        public string CodeLocation { get; set; }

        [StringLength(50)]
        public string Changesets { get; set; }

        [StringLength(50)]
        public string DataScriptNames { get; set; }

        [StringLength(50)]
        public string TFSWorkItems { get; set; }


        [StringLength(20)]
        public string DeploymentCycle { get; set; }

        public bool? TestableInQA { get; set; }

        [Required]
        //Added for convenience
        public bool TestableInQABool { get; set; }

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

        [Required]
        //Added for convenience
        public bool TestableInQTSBool { get; set; }

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