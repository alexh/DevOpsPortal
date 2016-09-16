namespace DevOpsPortal.Models
{
    public class DeploymentChartsStats
    {
        public string Name { get; set; }
        public int TotalCount { get; set; }

        public int SuccessCount { get; set; }

        public int FailedCount { get; set; }
        public int CanceledCount { get; set; }

        public int TimedOutCount { get; set; }

        //Counts
        public int TotalSuccessCount7 { get; set; }

        public int DevCount7 { get; set; }
        public int ProductionCount7 { get; set; }

        public int QTSCount7 { get; set; }

        public int QACount7 { get; set; }

        public int BetaCount7 { get; set; }

        public int TotalSuccessCount14 { get; set; }
        public int DevCount14 { get; set; }
        public int ProductionCount14 { get; set; }

        public int QTSCount14 { get; set; }

        public int QACount14 { get; set; }

        public int BetaCount14 { get; set; }

        public string NamesString { get; set; }
        public string ProductionString { get; set; }
        public string QTSString { get; set; }
        public string QAString { get; set; }
        public string BetaString { get; set; }

        public string DevLineData { get; set; }
        public string QALineData { get; set; }
        public string QTSLineData { get; set; }
        public string BetaLineData { get; set; }
        public string ProductionLineData { get; set; }

        public int[] RollBackData { get; set; }
    }
}