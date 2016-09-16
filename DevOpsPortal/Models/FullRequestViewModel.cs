using System;
using System.ComponentModel.DataAnnotations;

namespace DevOpsPortal.Models {
    public class FullRequestViewModel {
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

        [Required]
        //Added for convenience
        public DateTime DeployDateTimeNotNull { get; set; }

        [Required]
        //Added for convenience
        public String DeployTime { get; set; }

        public DateTime? DeployDateTime { get; set; }

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

        [Required]
        [StringLength(20)]
        public string DeploymentCycle { get; set; }

        [StringLength(50)]
        public string DataScriptNames { get; set; }

        public bool? TestableInQADB { get; set; }

        [Required]
        //Added for convenience
        public bool TestableInQABoolDB { get; set; }

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

        [Required]
        //Added for convenience
        public bool TestableInQTSBoolDB { get; set; }

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

        [StringLength(50)]
        public string QTSTesterDB { get; set; }
    }
}