using System.DirectoryServices.AccountManagement;

namespace DevOpsPortal.Models
{
    public class ADUserData
    {
        public string EmployeeId { get; set; }
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public ADUserData(UserPrincipal p)
        {
            this.DisplayName = p.DisplayName;
            this.Description = p.Description;
            this.EmailAddress = p.EmailAddress;
            this.Name = p.Name;
            this.MiddleName = p.MiddleName;
        }
    }
}