namespace DevOpsPortal.Models
{
    public class AddDBDeployRequestReturn
    {
        public AddDBDeployRequestReturn(DBDeployRequestViewModel model, bool successful)
        {
            this.model = model;
            this.successful = successful;
        }

        public DBDeployRequestViewModel model { get; set; }

        public bool successful { get; set; }
    }
}