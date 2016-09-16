namespace DevOpsPortal.Models
{
    public class RollbackData
    {
        public string TeamName { get; set; }
        public int Count { get; set; }

        public RollbackData()
        {
        }

        public RollbackData(string TeamName, int Count)
        {
            this.TeamName = TeamName;
            this.Count = Count;
        }
    }
}