using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevOpsPortal.Models;

namespace DevOpsPortal.Controllers {
    public class RequestController : Controller {
        CodeDeployRequestDbContext cdbc = new CodeDeployRequestDbContext();
        DBDeployDbContext ddbc = new DBDeployDbContext();

        public JsonResult GetRequests(string AppName, string type) {
            if (type == "Code") {
                return this.GetCodeRequests(AppName);
            }
            if (type == "DB") {
                return this.GetDBRequests(AppName);
            }
            if (type == "Full") {
                return this.GetFullRequests(AppName);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCodeRequests(string AppName) {

            var codes = from m in cdbc.CodeDeployRequests
                        where (m.AppName == AppName)
                        select m;
            foreach (var c in codes) {
                c.Team = c.Team.TrimEnd();
                c.AppName = c.AppName.TrimEnd();
            }
            return Json(codes.AsEnumerable(), JsonRequestBehavior.AllowGet);
        }


        //AppName is really the Team for SQL only requests because there is no AppName field that is controlled with a drop down on a SQL only request
        public JsonResult GetDBRequests(string AppName) {

            var dbs = from m in ddbc.DBDeployRequests
                      where (m.Team == AppName)
                      select m;
            foreach (var d in dbs) {
                d.Team = d.Team.TrimEnd();

            }
            return Json(dbs.AsEnumerable().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFullRequests(string AppName) {
            AppName = AppName.TrimEnd();


            var codes = from m in cdbc.CodeDeployRequests
                        where (m.AppName == AppName && m.DBDeployRequestID != null)
                        select m;
            var dbs = from m in ddbc.DBDeployRequests
                      select m;
            List<FullRequest> fulls = new List<FullRequest>();
            foreach (var c in codes) {
                c.AppName = c.AppName.TrimEnd();
                c.Team = c.Team.TrimEnd();
                int code_ID = c.CodeDeployRequestID;
                var db = dbs.Where(m => m.CodeDeployRequestID == code_ID).FirstOrDefault();
                FullRequest thisFull = new FullRequest(c, db);
                fulls.Add(thisFull);
            }
            return Json(fulls, JsonRequestBehavior.AllowGet);
        }


    }
}