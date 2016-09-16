using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System.Net;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers
{
    public class ApplicationsController : BaseController
    {
        // GET: Applications
        public override ActionResult Index()
        {
            ViewBag.PageName = "Applications";
            base.Index();
            return View();
        }

        public JsonResult GetApplications()
        {
            return Json(ApplicationManager.GetApplicationsFromMasterProgramInventory(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistinctApplications()
        {
            return Json(ApplicationManager.GetDistinctApplications(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillApps(string team)
        {
            return Json(ApplicationManager.GetApplicationsForTeam(team), JsonRequestBehavior.AllowGet);
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = ApplicationManager.GetApplication(id.Value);
            if (application == null)
            {
                return View("../Shared/NotFound");
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "App_ID,Environment,Program,Machine,BaseFolder,Instances,DeploymentServer,ServiceScheduled,Team,TeamName,Comments,Column9,IsAutomated,JenkinsBuild,OctopusDeploy,ConfigStructure,SolutionStructure,JenkinsName,OctopusName,ApplicationTypeId,Sunsetted")] Application application)
        {
            if (ModelState.IsValid)
            {
                ApplicationManager.SaveApplication(application);
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = ApplicationManager.GetApplication(id.Value);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "App_ID,Environment,Program,Machine,BaseFolder,Instances,DeploymentServer,ServiceScheduled,Team,TeamName,Comments,Column9,IsAutomated,JenkinsBuild,OctopusDeploy,ConfigStructure,SolutionStructure,JenkinsName,OctopusName,ApplicationTypeId,Sunsetted")] Application application)
        {
            if (ModelState.IsValid)
            {
                ApplicationManager.UpdateApplication(application);
                return RedirectToAction("Index");
            }
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = ApplicationManager.GetApplication(id.Value);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationManager.DeleteApplication(id);
            return RedirectToAction("Index");
        }
    }
}