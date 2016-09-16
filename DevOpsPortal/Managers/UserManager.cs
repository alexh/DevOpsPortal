using DevOpsPortal.Models;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Hosting;

namespace DevOpsPortal.Managers {
    public static class UserManager {
        private static TeamDbContext db = new TeamDbContext();
        private static UserDbContext udb = new UserDbContext();

        public static User GetMyUser(string userName) {
            using (HostingEnvironment.Impersonate()) {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain)) {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, userName);
                    ADUserData ud = new ADUserData(up);

                    //string displayName = ud.DisplayName.ToLower();
                    //displayName = displayName.Replace(" ", "");
                    //var names = displayName.Split(',');
                    //string firstName = names.Last();
                    //string lastName = names.First();
                    string firstName = up.GivenName;
                    string lastName = up.Surname;

                    //string username = User.Identity.Name.Substring(User.Identity.Name.IndexOf('\\') + 1);

                    var users = from m in udb.MasterUserInventories
                                where (m.FirstName.ToLower().Equals(firstName) && m.LastName.ToLower().Equals(lastName))
                                select m;
                    if (users.ToArray().Length == 0) {
                        return null;
                    } else {
                        var user = users.AsEnumerable().First();
                        return user;
                    }
                }
            }
        }

        public static string GetName(int id) {
            var users = from m in udb.MasterUserInventories
                        where (m.User_ID == id)
                        select m;
            var user = users.AsEnumerable().First();
            if (user == null) {
                return string.Empty;
            } else {
                return user.FirstName.TrimEnd(' ') + " " + user.LastName.TrimEnd(' ');
            }
        }
    }
}