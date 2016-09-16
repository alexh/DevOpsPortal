using DevOpsPortal.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Hosting;

namespace DevOpsPortal.Managers
{
    public static class TeamManager
    {
        private static ApplicationDbContext app = new ApplicationDbContext();
        private static TeamDbContext db = new TeamDbContext();
        private static UserDbContext users = new UserDbContext();

        public static List<string> GetTeamNames()
        {
            var names = from m in app.MasterProgramInventories
                        where (!m.Sunsetted && m.Team != "DevOps" && m.Team != "Infrastructure")
                        orderby m.Program, m.Environment, m.Team, m.IsAutomated
                        select m.Team;
            return names.Distinct().ToList();
        }

        public static string GetMyTeams(string userIdentity)
        {
            return GetMyUser(userIdentity).Team;
        }

        public static Models.User GetMyUser(string userIdentity)
        {
            UserDbContext udb = new UserDbContext();
            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, userIdentity);
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
                    if (users.ToArray().Length == 0)
                    {
                        return null;
                    }
                    else
                    {
                        var user = users.AsEnumerable().First();
                        return user;
                    }
                }
            }
        }

        public static string GetEmail(string team)
        {
            var teams = from m in db.MasterTeamInventories
                        where (m.TeamFunction == team)
                        select m.EmailDistibution;
            if (teams == null || teams.First() == null)
            {
                return string.Empty;
            }
            return teams.First().TrimEnd();
        }

        public static Team GetTeam(int id)
        {
            return db.MasterTeamInventories.Find(id);
        }

        public static List<Team> GetTeams()
        {
            var teams = (from m in db.MasterTeamInventories
                        orderby m.TeamName
                        select m).ToList();

            return teams;
        }

        public static string GetTeamName(int id)
        {
            var teams = from m in db.MasterTeamInventories
                        where (m.Team_ID == id)
                        select m.TeamFunction;
            var team = teams.First();
            return team.TrimEnd(' ');
        }

        public static int GetMyTeamID(string userName)
        {
            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    using (UserDbContext dba = new UserDbContext())
                    {
                        UserPrincipal up = UserPrincipal.FindByIdentity(pc, userName);
                        ADUserData ud = new ADUserData(up);

                        string displayName = ud.DisplayName.ToLower();
                        displayName = displayName.Replace(" ", "");
                        var names = displayName.Split(',');
                        string firstName = names.Last();
                        string lastName = names.First();

                        var users = from m in dba.MasterUserInventories
                                    where (m.FirstName.ToLower().Equals(firstName) && m.LastName.ToLower().Equals(lastName))
                                    select m;
                        if (users.ToArray().Length == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            var user = users.AsEnumerable().First();
                            return user.User_ID;
                        }
                    }
                }
            }
        }

        //public static List<TeamWithNames> GetTeamsWithStaffNames(string t, User user)
        //{
        //    string[] teamNames = null;
        //    if (t != null)
        //    {
        //        teamNames = t.Split(',');
        //    }

        //    List<TeamWithNames> list = new List<TeamWithNames>();
        //    var teams = db.Database.SqlQuery<TeamWithNames>("SELECT [Team_ID] , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.AgileLead) as AgileFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.AgileLead) as AgileLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.ProductManager) as ProductManagerFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.ProductManager) as ProductManagerLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.OperationReadiness) as ORFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.OperationReadiness) as ORLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.Architect) as ArchFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.Architect) as ArchLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.[BA]) as BAFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.[BA]) as BALast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.TeamLead) as LeadFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.TeamLead) as LeadLast,[Team]  as TeamFunction,[TeamName],[AgileLead],[ProductManager],[OperationReadiness],[Architect],[BA],[QA],[TeamLead],[Development],[EmailDistibution],[Notes],[StandupTime],[StandupDays] FROM[dbo].[MasterTeamInventory] as t").AsEnumerable();
        //    foreach (TeamWithNames team in teams)
        //    {
        //        string qa = team.QA;

        //        List<String> QaNames = new List<string>();
                
        //        if (qa != null)
        //        {
        //            if (qa.Contains(","))
        //            {
        //                foreach (string id in qa.Split(','))
        //                {
        //                    string name = user.GetName(Int32.Parse(id.TrimEnd(' ')));
        //                    if (team.QANames == null)
        //                    {
        //                        team.QANames = name;
        //                    }
        //                    else
        //                    {
        //                        team.QANames = team.QANames + ", " + name;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                team.QANames = user.GetName(Int32.Parse(team.QA));
        //            }
        //        }
        //        if (t == null)
        //        {
        //            list.Add(team);
        //        }
        //        else
        //        {
        //            if (teamNames.Contains(team.Team_ID.ToString()))
        //            {
        //                list.Add(team);
        //            }
        //        }
        //    }

        //}
    }
}