namespace DevOpsPortal.Models
{
    public class EnvJSON
    {
        public string EnvName { get; set; }

        public EnvJSON(string name)
        {
            EnvName = name;
        }
    }
}