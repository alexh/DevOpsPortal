using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers {
    public class DeploymentsController : BaseController {
        // GET: Deployments
        public override ActionResult Index() {
            ViewBag.PageName = "Deployments";
            base.Index();
            return View();
        }

        // GET: Deployments/Details/5
        public ActionResult Details(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deployment deployment = DeploymentsManager.GetDeployment(id);
            if (deployment == null) {
                return View("../Shared/NotFound");
            }
            return View(deployment);
        }


        public JsonResult GetDeployments() {
            var toReturn = Json(DeploymentsManager.GetDeployments(), JsonRequestBehavior.AllowGet);
            toReturn.MaxJsonLength = Int32.MaxValue;
            return toReturn;
        }

        //GET Deployment Status Data
        public ActionResult GetDeploymentStatusData() {
            var list = DeploymentsManager.GetDeploymentStatus();
            ViewBag.TestCount = list.Count();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Status
        public ActionResult Status() {
            ViewBag.PageName = "Status";
            base.Index();
            return View();
        }

        //Charts
        public ActionResult Charts() {
            return PartialView(DeploymentChartStatsManager.GetDeploymentChartStats());
        }

        public ActionResult Statistics() {
            ViewBag.PageName = "Statistics";
            base.Index();
            return View();
        }
    }
}