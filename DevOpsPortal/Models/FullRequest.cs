using System;
using System.ComponentModel.DataAnnotations;

namespace DevOpsPortal.Models {
    public class FullRequest {


        public FullRequest() {

        }

        public FullRequest(CodeDeployRequest c, DBDeployRequest db) {
            this.AppName = c.AppName;
            this.Changesets = db.Changesets;
            this.CodeDeployRequestID = c.CodeDeployRequestID;
            this.CodeLocation = db.CodeLocation;
            this.ConfirmationEmail = c.ConfirmationEmail;
            this.DatabaseName = db.DatabaseName;
            this.DataScriptNames = db.DataScriptNames;
            this.DBDeployRequestID = db.DBDeployRequestID;
            this.DeployDateTime = c.DeployDateTime;
            this.DeploymentCycle = db.DeploymentCycle;
            this.DeployType = c.DeployType;
            this.DINumber = c.DINumber;
            this.Environment = c.Environment;
            this.JenkinsBuild = c.JenkinsBuild;
            this.QADataImpact = c.QADataImpact;
            this.QADataImpactDB = db.QADataImpact;
            this.QAFailureImpact = c.QAFailureImpact;
            this.QAFailureImpactDB = db.QAFailureImpact;
            this.QAFailureResolution = c.QAFailureResolution;
            this.QAFailureResolutionDB = db.QAFailureResolution;
            this.QAIdentifyFailure = c.QAIdentifyFailure;
            this.QAIdentifyFailureDB = db.QAIdentifyFailure;
            this.QAMitigation = c.QAMitigation;
            this.QAMitigationDB = db.QAMitigation;
            this.QAReason = c.QAReason;
            this.QAReasonDB = db.QAReason;
            this.QATester = c.QATester;
            this.QATesterDB = db.QATester;
            this.QTSDataImpact = c.QTSDataImpact;
            this.QTSDataImpactDB = db.QTSDataImpact;
            this.QTSFailureImpact = c.QTSFailureImpact;
            this.QTSFailureImpactDB = db.QTSFailureImpact;
            this.QTSFailureResolution = c.QTSFailureResolution;
            this.QTSFailureResolutionDB = db.QTSFailureResolution;
            this.QTSIdentifyFailure = c.QTSIdentifyFailure;
            this.QTSIdentifyFailureDB = db.QTSIdentifyFailure;
            this.QTSMitigation = c.QTSMitigation;
            this.QTSMitigationDB = db.QTSMitigation;
            this.QTSReason = c.QTSReason;
            this.QTSReasonDB = db.QTSReason;
            this.SpecialNotes = c.SpecialNotes;
            this.SubmissionTime = c.SubmissionTime;
            this.Team = c.Team;
            this.TestableInQA = c.TestableInQA;
            this.TestableInQADB = db.TestableInQA;
            this.TestableInQTS = c.TestableInQTS;
            this.TestableInQTSDB = db.TestableInQTS;
            this.TFSWorkItems = c.TFSWorkItems;
            this.TFSWorkItemsDB = db.TFSWorkItems;
            this.User = c.User;



        }

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

        //DB


        [StringLength(50)]
        public string DatabaseName { get; set; }

        [StringLength(50)]
        public string CodeLocation { get; set; }

        [StringLength(50)]
        public string Changesets { get; set; }


        [StringLength(50)]
        public string TFSWorkItemsDB { get; set; }


        [StringLength(20)]
        public string DeploymentCycle { get; set; }

        [StringLength(50)]
        public string DataScriptNames { get; set; }

        public bool? TestableInQADB { get; set; }

        [StringLength(500)]
        public string QAReasonDB { get; set; }

        [StringLength(500)]
        public string QAMitigationDB { get; set; }

        [StringLength(500)]
        public string QAIdentifyFailureDB { get; set; }

        [StringLength(500)]
        public string QAFailureImpactDB { get; set; }

        [StringLength(500)]
        public string QADataImpactDB { get; set; }

        [StringLength(500)]
        public string QAFailureResolutionDB { get; set; }

        [StringLength(50)]
        public string QATesterDB { get; set; }

        public bool? TestableInQTSDB { get; set; }

        [StringLength(500)]
        public string QTSReasonDB { get; set; }

        [StringLength(500)]
        public string QTSMitigationDB { get; set; }

        [StringLength(500)]
        public string QTSIdentifyFailureDB { get; set; }

        [StringLength(500)]
        public string QTSFailureImpactDB { get; set; }

        [StringLength(500)]
        public string QTSDataImpactDB { get; set; }

        [StringLength(500)]
        public string QTSFailureResolutionDB { get; set; }

    }
}