using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers {
    public class TeamsController : BaseController {
        private TeamDbContext db = new TeamDbContext();
        private UserDbContext users = new UserDbContext();

        // GET: Teams
        public override ActionResult Index() {
            ViewBag.PageName = "Teams";
            base.Index();
            return View();
        }

        public ActionResult GetEmail(string team) {
            return Json(TeamManager.GetEmail(team), JsonRequestBehavior.AllowGet);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = TeamManager.GetTeam(id.Value);
            if (team == null) {
                return HttpNotFound();
            }
            return View(team);
        }

        public string GetMyTeams() {
            UserDbContext udb = new UserDbContext();
            using (HostingEnvironment.Impersonate()) {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain)) {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, User.Identity.Name);
                    ADUserData ud = new ADUserData(up);

                    string displayName = ud.DisplayName.ToLower();
                    displayName = displayName.Replace(" ", "");
                    var names = displayName.Split(',');
                    string firstName = names.Last();
                    string lastName = names.First();

                    string username = User.Identity.Name.Substring(User.Identity.Name.IndexOf('\\') + 1);

                    var users = from m in udb.MasterUserInventories
                                where (m.FirstName.ToLower().Equals(firstName) && m.LastName.ToLower().Equals(lastName))
                                select m;
                    if (users.ToArray().Length == 0) {
                        return null;
                    } else {
                        var user = users.AsEnumerable().First();
                        return user.Team;
                    }
                }
            }
        }

        public ActionResult GetValidEnvironments(string Team, string User) {
            User user = this.GetMyUser();

            //Full control for DevOps
            if (user.Team.TrimEnd() == "5") {
                List<EnvJSON> list = new List<EnvJSON>();
                list.Add(new EnvJSON("QA"));
                list.Add(new EnvJSON("QTS"));
                list.Add(new EnvJSON("Production"));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            if (Team == "Select Team") {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var team = db.MasterTeamInventories.Where(m => m.TeamFunction == Team);
            if (team != null && team.Count() > 0) {
                var myTeam = team.First();
                int userId = user.User_ID;
                List<EnvJSON> list = new List<EnvJSON>();
                if (myTeam.TeamLead == userId) {
                    //Add QA
                    list.Add(new EnvJSON("QA"));
                }
                string[] qa = myTeam.QA.Split(',');

                // if on QA for team, add QTS and Prod
                foreach (string q in qa) {
                    if (Int32.Parse(q) == userId) {
                        list.Add(new EnvJSON("QTS"));
                        list.Add(new EnvJSON("Production"));
                    }
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            } else {
                return Json(new List<EnvJSON>(), JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetQAs(string Team, string User) {
            User = User.TrimEnd();
            int UserID = Int32.Parse(User);
            Team = Team.TrimEnd();
            List<BasicUser> toReturn = new List<BasicUser>();
            TeamDbContext tc = new TeamDbContext();
            // UserManager.GetName()
            bool isQA = false;
            var teams = tc.MasterTeamInventories.Where(m => m.TeamFunction == Team);
            Team t = teams.First();
            if (t.QA == null) {
                return Json(toReturn, JsonRequestBehavior.AllowGet);
            }
            string[] qa = t.QA.Split(',');
            foreach (string q in qa) {
                if (q.TrimEnd().Equals(User)) {
                    isQA = true;
                }
            }

            if (isQA) {
                toReturn.Add(new BasicUser(this.GetMyUser()));
            } else {
                foreach (string q in qa) {
                    BasicUser bu = new BasicUser();
                    bu.Name = UserManager.GetName(Int32.Parse(q.TrimEnd()));
                    bu.ID = q.TrimEnd();
                    toReturn.Add(bu);
                }
            }

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }





        public Models.User GetMyUser() {
            UserDbContext udb = new UserDbContext();
            using (HostingEnvironment.Impersonate()) {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain)) {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, User.Identity.Name);
                    ADUserData ud = new ADUserData(up);
                    string firstName = up.GivenName;
                    string lastName = up.Surname;

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

        public JsonResult GetMyTeamsJSON() {
            UserController user = new UserController();
            string teamsString = user.GetMyTeams();
            string[] teamsArray = teamsString.Split(',');

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeamsJSON() {
            return Json(TeamManager.GetTeams(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTeamsWithStaffNamesFromList(IEnumerable<TeamWithNames> teams) {
            List<TeamWithNames> list = new List<TeamWithNames>();
            foreach (TeamWithNames team in teams) {
                string qa = team.QA;
                List<String> QaNames = new List<string>();
                if (qa != null) {
                    if (qa.Contains(",")) {
                        foreach (string id in qa.Split(',')) {
                            string name = UserManager.GetName(Int32.Parse(id.TrimEnd(' ')));
                            if (team.QANames == null) {
                                team.QANames = name;
                            } else {
                                team.QANames = team.QANames + ", " + name;
                            }
                        }
                    } else {
                        team.QANames = UserManager.GetName(Int32.Parse(team.QA));
                    }
                }
                list.Add(team);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeamsWithStaffNames(string t) {
            string[] teamNames = null;
            if (t != null) {
                teamNames = t.Split(',');
            }

            List<TeamWithNames> list = new List<TeamWithNames>();
            var teams = db.Database.SqlQuery<TeamWithNames>("SELECT [Team_ID] , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.AgileLead) as AgileFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.AgileLead) as AgileLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.ProductManager) as ProductManagerFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.ProductManager) as ProductManagerLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.OperationReadiness) as ORFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.OperationReadiness) as ORLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.Architect) as ArchFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.Architect) as ArchLast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.[BA]) as BAFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.[BA]) as BALast , (SELECT q.FirstName FROM MasterUserInventory as q WHERE User_ID = t.TeamLead) as LeadFirst , (SELECT q.LastName FROM MasterUserInventory as q WHERE User_ID = t.TeamLead) as LeadLast,[Team]  as TeamFunction,[TeamName],[AgileLead],[ProductManager],[OperationReadiness],[Architect],[BA],[QA],[TeamLead],[Development],[EmailDistibution],[Notes],[StandupTime],[StandupDays] FROM[dbo].[MasterTeamInventory] as t").AsEnumerable();
            foreach (TeamWithNames team in teams) {
                string qa = team.QA;

                List<String> QaNames = new List<string>();

                if (qa != null) {
                    if (qa.Contains(",")) {
                        foreach (string id in qa.Split(',')) {
                            string name = UserManager.GetName(Int32.Parse(id.TrimEnd(' ')));
                            if (team.QANames == null) {
                                team.QANames = name;
                            } else {
                                team.QANames = team.QANames + ", " + name;
                            }
                        }
                    } else {
                        team.QANames = UserManager.GetName(Int32.Parse(team.QA));
                    }
                }
                if (t == null) {
                    list.Add(team);
                } else {
                    if (teamNames.Contains(team.Team_ID.ToString())) {
                        list.Add(team);
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Team_ID,TeamFunction,TeamName,AgileLead,ProductManager,OperationReadiness,Architect,BA_AssoPM,QA,TeamLead,Development,EmailDistibution,Notes,StandupTime,StandupDays")] Team team) {
            if (ModelState.IsValid) {
                db.MasterTeamInventories.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = TeamManager.GetTeam(id.Value);
            if (team == null) {
                return HttpNotFound();
            }
            //using (UserController uc = new UserController()) {
            //    ViewBag.UserId = uc.GetMyID();
            //}
            int? user = TeamManager.GetMyTeamID(User.Identity.Name);
            user = 210;
            ViewBag.UserId = user;

            if (this.IsValidEditor(user, team)) {
                return View(team);
            } else {
                return View("../Shared/CantEdit");
            }
        }

        public Boolean IsValidEditor(int? user, Team team) {
            return (user != null && (team.AgileLead == user || team.ProductManager == user || team.TeamLead == user || team.BA == user));
        }


        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Team_ID,TeamFunction,TeamName,AgileLead,ProductManager,OperationReadiness,Architect,BA_AssoPM,QA,TeamLead,Development,EmailDistibution,Notes,StandupTime,StandupDays")] Team team) {
            if (ModelState.IsValid) {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.MasterTeamInventories.Find(id);
            if (team == null) {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Team team = db.MasterTeamInventories.Find(id);
            db.MasterTeamInventories.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}