namespace DevOpsPortal
{
    public class ProgramDeployment
    {
        public string Program { get; set; }
        public int Production { get; set; }
        public int QTS { get; set; }
        public int QA { get; set; }
        public int Beta { get; set; }
    }
}