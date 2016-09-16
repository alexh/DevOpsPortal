using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers
{
    public class UserController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: User
        public override ActionResult Index()
        {
            base.Index();
            return Account();
        }

        public ActionResult Details(string id)
        {
            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    string fullname = "AUTOTRADER\\x";
                    fullname = fullname.Replace("x", "");
                    fullname = fullname + id;
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, fullname);
                    if (up == null)
                    {
                        return View("../Shared/NotFound");
                    }
                    else
                    {
                        return View(new ADUserData(up));
                    }
                }
            }
        }

        public ActionResult IDDetails(int id)
        {
            var users = from m in db.MasterUserInventories
                        where (m.User_ID == id)
                        select m;
            if (users.ToArray().Length == 0)
            {
                return View("../Shared/NotFound");
            }
            User user = users.First();
            string teams = user.Team;
            var teamArr = teams.Split(',');
            if (teamArr.Length > 0)
            {
                string teamNames = "";
                TeamsController tc = new TeamsController();
                foreach (string t in teamArr)
                {
                    string name = TeamManager.GetTeamName(Int32.Parse(t));
                    if (teamNames.Equals(""))
                    {
                        teamNames = name;
                    }
                    else
                    {
                        teamNames = teamNames + ", " + name;
                    }
                }
                user.Team = teamNames;
                return View(user);
            }
            return View(user);
        }

        public JsonResult GetUsers()
        {
            var users = from m in db.MasterUserInventories
                        select m;
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Lookup()
        {
            return View();
        }

        public JsonResult GetTeamsJSON()
        {
            List<Team> teamList = new List<Team>();
            List<int> teamIds = new List<int>();
            string teamsString = this.GetMyTeams();
            if (teamsString != null)
            {
                var teams = teamsString.Split(',');
                using (TeamDbContext tdb = new TeamDbContext())
                {
                    foreach (string team in teams)
                    {
                        int teamNum = Int32.Parse(team);
                        var curr = from m in tdb.MasterTeamInventories
                                   where (m.Team_ID == teamNum)
                                   select m;
                        foreach (Team t in curr)
                        {
                            teamList.Add(t);
                            teamIds.Add(t.Team_ID);
                        }
                    }
                }
            }
            var teamsQ = db.Database.SqlQuery<TeamWithNames>("SELECT [Team_ID] , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.AgileLead) as AgileFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.AgileLead) as AgileLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.ProductManager) as ProductManagerFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.ProductManager) as ProductManagerLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.OperationReadiness) as ORFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.OperationReadiness) as ORLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.Architect) as ArchFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.Architect) as ArchLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.[BA]) as BAFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.[BA]) as BALast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.TeamLead) as LeadFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.TeamLead) as LeadLast,[Team],[TeamName],[AgileLead],[ProductManager],[OperationReadiness],[Architect],[BA],[QA],[TeamLead],[Development],[EmailDistibution],[Notes],[StandupTime],[StandupDays] FROM[dbo].[MasterTeamInventory] as t").AsEnumerable();
            teamsQ = teamsQ.Where(t => teamIds.Contains(t.Team_ID));
            TeamsController tc = new TeamsController();
            return tc.GetTeamsWithStaffNamesFromList(teamsQ);
        }

        public ActionResult MyTeams()
        {
            ViewBag.PageName = "MyTeams";
            return RedirectToRoute(new
            {
                controller = "Teams",
                action = "Index",
                t = this.GetMyTeams(),
            });
        }

        public ActionResult MyApps()
        {
            ViewBag.PageName = "MyApps";
            return View();
        }

        public ActionResult MyDeployments()
        {
            ViewBag.PageName = "MyDeployments";
            return View();
        }

        // Returns current user's user object
        public Models.User GetMyUser()
        {
            UserDbContext udb = new UserDbContext();
            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, User.Identity.Name);
                    ADUserData ud = new ADUserData(up);
                    string firstName = up.GivenName;
                    string lastName = up.Surname;
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

        public JsonResult GetMyApps()
        {
            var db = new ApplicationDbContext();
            var teamsString = this.GetMyTeams();
            if (teamsString == null)
            {
                teamsString = "";
            }
            string[] teams = teamsString.Split(',');
            var apps = from m in db.MasterProgramInventories
                       where (!m.Sunsetted && m.Team != "DevOps" && teams.Contains(m.Team))
                       orderby m.Program, m.Environment
                       select m;

            return Json(apps, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyDeployments()
        {
            var ddb = new DeploymentDbContext();
            var adb = new ApplicationDbContext();
            var tdb = new Model1();
            var tc = new TeamsController();
            var teamsString = this.GetMyTeams();
            if (teamsString == null)
            {
                teamsString = "";
            }
            string[] teams = teamsString.Split(',');
            string query = "SELECT p.Team ,[DeploymentId] ,[DeploymentName] ,[ProjectId] ,[ProjectName] ,[ProjectSlug] ,[EnvironmentId] ,[EnvironmentName] ,[ReleaseId] ,[ReleaseVersion] ,[TaskId] ,[TaskState] ,[Created] ,[QueueTime] ,[StartTime] ,[CompletedTime] ,[DurationSeconds] ,[DeployedBy] FROM[dbo].[DeploymentHistory] as d JOIN[dbo].[MasterProgramInventory] as p ON d.ProjectName = p.Program";
            if (teams.Length > 0)
            {
                query = query + " WHERE ";
                foreach (string t in teams)
                {
                    query = query + " Team = '" + TeamManager.GetTeamName(Convert.ToInt32(t)) + "' OR";
                }
                query = query.Substring(0, query.LastIndexOf(' '));
            }
            var deps = tdb.Database.SqlQuery<DeploymentWithTeam>(query).AsEnumerable();
            List<DeploymentWithTeamForJSON> list = new List<DeploymentWithTeamForJSON>();
            foreach (var d in deps)
            {
                list.Add(new DeploymentWithTeamForJSON(d));
            }
            ViewBag.Q = query;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public int? GetMyID()
        {
            return this.GetMyUser().User_ID;
        }

        //Get Teams from Current Users as string
        public string GetMyTeams()
        {
            return this.GetMyUser().Team.TrimEnd();
        }

        public ActionResult ADAccount()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                string username = User.Identity.Name.Substring(User.Identity.Name.IndexOf('\\') + 1);
                UserPrincipal up = UserPrincipal.FindByIdentity(pc, username);
                ADUserData ud = new ADUserData(up);
                return PartialView(ud);
            }
        }

        public ActionResult Edit()
        {
            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, User.Identity.Name);
                    ADUserData ud = new ADUserData(up);

                    //string displayName = ud.DisplayName.ToLower();
                    //displayName = displayName.Replace(" ", "");
                    //var names = displayName.Split(',');
                    //string firstName = names.Last();
                    //string lastName = names.First();
                    string firstName = up.GivenName;
                    string lastName = up.Surname;

                    string username = User.Identity.Name.Substring(User.Identity.Name.IndexOf('\\') + 1);

                    var users = from m in db.MasterUserInventories
                                where (m.FirstName.ToLower().Equals(firstName) && m.LastName.ToLower().Equals(lastName))
                                select m;
                    if (users.ToArray().Length == 0)
                    {
                        return View("../Shared/NotFound");
                    }
                    else
                    {
                        var user = users.AsEnumerable().First();
                        return View(user);
                    }
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Account");
            }
            return View(user);
        }

        public ActionResult Account()
        {
            using (HostingEnvironment.Impersonate())
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, User.Identity.Name);
                    ADUserData ud = new ADUserData(up);

                    //string displayName = ud.DisplayName.ToLower();
                    //displayName = displayName.Replace(" ", "");
                    //var names = displayName.Split(',');
                    //string firstName = names.Last();
                    //string lastName = names.First();
                    string firstName = up.GivenName;
                    string lastName = up.Surname;

                    string username = User.Identity.Name.Substring(User.Identity.Name.IndexOf('\\') + 1);

                    var users = from m in db.MasterUserInventories
                                where (m.FirstName.ToLower().Equals(firstName) && m.LastName.ToLower().Equals(lastName))
                                select m;
                    if (users.ToArray().Length == 0)
                    {
                        return View("../Shared/NotFound");
                    }
                    else
                    {
                        var user = users.AsEnumerable().First();
                        return View(user);
                    }
                }
            }
        }
    }
}