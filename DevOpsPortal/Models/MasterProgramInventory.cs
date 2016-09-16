namespace DevOpsPortal.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MasterProgramInventory")]
    public partial class MasterProgramInventory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int App_ID { get; set; }

        [StringLength(50)]
        public string Environment { get; set; }

        [StringLength(150)]
        public string Program { get; set; }

        [StringLength(50)]
        public string Machine { get; set; }

        [StringLength(50)]
        public string BaseFolder { get; set; }

        [StringLength(50)]
        public string Instances { get; set; }

        [StringLength(50)]
        public string DeploymentServer { get; set; }

        [StringLength(50)]
        public string ServiceScheduled { get; set; }

        [StringLength(50)]
        public string Team { get; set; }

        [StringLength(50)]
        public string TeamName { get; set; }

        [StringLength(150)]
        public string Comments { get; set; }

        public bool? IsAutomated { get; set; }

        public bool? JenkinsBuild { get; set; }

        public bool? OctopusDeploy { get; set; }

        public bool? ConfigStructure { get; set; }

        public bool? SolutionStructure { get; set; }

        [StringLength(150)]
        public string JenkinsName { get; set; }

        [StringLength(150)]
        public string OctopusName { get; set; }

        public int? ApplicationTypeId { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool Sunsetted { get; set; }
    }
}