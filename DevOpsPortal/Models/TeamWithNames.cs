namespace DevOpsPortal.Models
{
    public partial class TeamWithNames
    {
        public int Team_ID { get; set; }

        public string AgileFirst { get; set; }
        public string AgileLast { get; set; }

        public string ProductManagerFirst { get; set; }
        public string ProductManagerLast { get; set; }

        public string ORFirst { get; set; }
        public string ORLast { get; set; }

        public string ArchFirst { get; set; }
        public string ArchLast { get; set; }

        public string BAFirst { get; set; }
        public string BALast { get; set; }

        public string LeadFirst { get; set; }
        public string LeadLast { get; set; }

        public string QANames { get; set; }

        public string TeamFunction { get; set; }

        public string TeamName { get; set; }

        public int? AgileLead { get; set; }

        public int? ProductManager { get; set; }

        public int? OperationReadiness { get; set; }

        public int? Architect { get; set; }

        public int? BA { get; set; }

        public string QA { get; set; }

        public int? TeamLead { get; set; }

        public string Development { get; set; }

        public string EmailDistibution { get; set; }

        public string Notes { get; set; }

        public string StandupTime { get; set; }

        public string StandupDays { get; set; }
    }
}