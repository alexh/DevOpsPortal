namespace DevOpsPortal.Models
{
    public class AddCodeDeployRequestReturn
    {
        public AddCodeDeployRequestReturn(CodeDeployRequestViewModel model, bool successful)
        {
            this.model = model;
            this.successful = successful;
        }

        public CodeDeployRequestViewModel model { get; set; }

        public bool successful { get; set; }
    }
}