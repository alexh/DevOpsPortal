using DevOpsPortal.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers
{
    public class EnvironmentRefreshController : BaseController
    {
        // GET: EnvironmentRefresh
        public override ActionResult Index()
        {
            ViewBag.Submitted = false;
            ViewBag.PageName = "Environment Refresh";
            ViewBag.Environments = EnvironmentManager.GetEnvironments();

            base.Index();

            return View();
        }

        [HttpPost]
        public ActionResult SubmitAction(FormCollection formColection)
        {
            ViewBag.Submitted = true;
            ViewBag.Environment = formColection["environment"].ToString();

            string environment = formColection["environment"].ToString();
            string nightTime = formColection["nightTime"].ToString();
            string dateTime = formColection["refreshDate"].ToString();

            bool isNightTime = false;
            DateTime deploymentDate = DateTime.Now;

            if(nightTime != string.Empty)
            {
                if (nightTime == "true")
                {
                    isNightTime = true;
                }
                else
                {
                    isNightTime = false;
                }
            }

            if(dateTime != string.Empty)
            {
                DateTime.TryParse(dateTime, out deploymentDate);
            }
            

            if(environment != string.Empty)
            {
                EnvironmentManager.RefreshEnvironment(environment, deploymentDate, isNightTime);
            }

            

            return View("Index");

        }
    }
}