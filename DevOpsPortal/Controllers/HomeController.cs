using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevOpsPortal.Models;
using System.Collections;
using System.DirectoryServices.AccountManagement;
using System.Web.Hosting;

namespace DevOpsPortal.Controllers {
    public class HomeController : BaseController {
        private ServerDBContext server = new ServerDBContext();
        private ApplicationDbContext app = new ApplicationDbContext();
        public override ActionResult Index() {
            base.Index();
            return RedirectToAction("Dashboard");
        }

        public ActionResult NotFound() {
            return View("../Shared/NotFound");
        }

        public ActionResult About() {

            return View();
        }
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }





        public ActionResult Dashboard() {
            ViewBag.PageName = "Dashboard";
            var names = from m in app.MasterProgramInventories
                        where (!m.Sunsetted && m.Team != "DevOps" && m.Team != "Infrastructure")
                        orderby m.Program, m.Environment, m.Team, m.IsAutomated
                        select m.Team;
            names = names.Distinct();
            string[] teams = names.ToArray();
            string[] environments = new string[] { "QA", "QTS", "Production" };
            this.SetUserTeam();

            ViewBag.Teams = teams;
            ViewBag.Environments = environments;



            return View();
        }


        public ActionResult Progress() {
            var apps = from m in app.MasterProgramInventories
                       where (!m.Sunsetted && m.Team != "DevOps")
                       orderby m.Program, m.Environment, m.Team, m.IsAutomated
                       select m;
            var names = from m in app.MasterProgramInventories
                        where (!m.Sunsetted && m.Team != "DevOps" && m.Team != "Infrastructure")
                        orderby m.Program, m.Environment, m.Team, m.IsAutomated
                        select m.Team;
            names = names.Distinct();
            string[] teams = names.ToArray();
            string[] environments = new string[] { "QA", "QTS", "Production" };
            HashSet<Progress> Progresses = new HashSet<Progress>();
            foreach (string team in teams) {

                Progress prog = new Progress();

                prog.Name = team;
                var currentTeam = apps.Select(app => new { app.Program, app.IsAutomated, app.Team, app.Environment }).Where(app => app.Team.Equals(team)).Where(app => app.Environment == "Production" || app.Environment == "QA" || app.Environment == "QTS").Distinct();

                prog.TotalCount = currentTeam.Count();

                prog.AutomatedCount = currentTeam.Where(app => app.IsAutomated == true).Count();


                prog.TotalPercent = (int) (((float) prog.AutomatedCount / (float) prog.TotalCount) * 100);

                prog.ProductionCount = currentTeam.Where(app => app.Environment.Equals("Production")).Count();
                prog.ProductionAutomatedCount = currentTeam.Where(app => app.Environment.Equals("Production") && app.IsAutomated == true).Count();
                prog.ProductionPercent = (int) (((float) prog.ProductionAutomatedCount / (float) prog.ProductionCount) * 100);

                prog.QTSCount = currentTeam.Where(app => app.Environment.Equals("QTS")).Count();
                prog.QTSAutomatedCount = currentTeam.Where(app => app.Environment.Equals("QTS") && app.IsAutomated == true).Count();
                prog.QTSPercent = (int) (((float) prog.QTSAutomatedCount / (float) prog.QTSCount) * 100);

                prog.QACount = currentTeam.Where(app => app.Environment.Equals("QA")).Count();
                prog.QAAutomatedCount = currentTeam.Where(app => app.Environment.Equals("QA") && app.IsAutomated == true).Count();
                prog.QAPercent = (int) (((float) prog.QAAutomatedCount / (float) prog.QACount) * 100);


                if (prog.ProductionPercent < 0)
                    prog.ProductionPercent = -1;

                if (prog.QTSPercent < 0)
                    prog.QTSPercent = -1;

                if (prog.QAPercent < 0)
                    prog.QAPercent = -1;
                Progresses.Add(prog);
            }
            return PartialView((IEnumerable) Progresses);

        }

        public ActionResult Counters() {
            CounterData data = new CounterData();
            var apps = from m in app.MasterProgramInventories
                       where (!m.Sunsetted && m.Team != "DevOps")
                       orderby m.Program, m.Environment, m.Team, m.IsAutomated
                       select m;
            var servers = from m in server.MasterServerInventories
                          orderby m.ComputerName
                          select m;
            data.ServerCount = servers.Count();
            data.AppCount = apps.Select(app => new { app.Program, app.IsAutomated }).Distinct().Count();
            data.AutoCount = apps.Where(app => app.IsAutomated == true).Select(app => app.Program).Distinct().Count();
            data.TotalPercent = (int) (((float) data.AutoCount / (float) data.AppCount) * 100);
            return PartialView(data);
        }
    }
}