namespace DevOpsPortal.Models
{
    public class Progress
    {
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public int AutomatedCount { get; set; }

        public int TotalPercent { get; set; }
        public int ProductionCount { get; set; }
        public int ProductionAutomatedCount { get; set; }
        public int ProductionPercent { get; set; }

        public int QTSCount { get; set; }
        public int QTSAutomatedCount { get; set; }
        public int QTSPercent { get; set; }

        public int QACount { get; set; }
        public int QAAutomatedCount { get; set; }
        public int QAPercent { get; set; }
    }
}