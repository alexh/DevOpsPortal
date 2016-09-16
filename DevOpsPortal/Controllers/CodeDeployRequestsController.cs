using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevOpsPortal.Models;

namespace DevOpsPortal.Controllers {
    public class CodeDeployRequestsController : Controller {
        private CodeDeployRequestDbContext db = new CodeDeployRequestDbContext();

        // GET: CodeDeployRequests
        public ActionResult Index() {
            return View(db.CodeDeployRequests.ToList());
        }

        // GET: CodeDeployRequests/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeDeployRequest codeDeployRequest = db.CodeDeployRequests.Find(id);
            if (codeDeployRequest == null) {
                return HttpNotFound();
            }
            return View(codeDeployRequest);
        }

        // GET: CodeDeployRequests/Create
        public ActionResult Create() {
            return View();
        }

        // POST: CodeDeployRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodeDeployRequestID,DBDeployRequestID,User,Team,Environment,AppName,DeployType,JenkinsBuild,DINumber,TFSWorkItems,DeployDateTime,TestableInQA,QAReason,QAMitigation,QAIdentifyFailure,QAFailureImpact,QADataImpact,QAFailureResolution,QATester,SpecialNotes,ConfirmationEmail,SubmissionTime")] CodeDeployRequest codeDeployRequest) {
            if (ModelState.IsValid) {
                db.CodeDeployRequests.Add(codeDeployRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codeDeployRequest);
        }

        // GET: CodeDeployRequests/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeDeployRequest codeDeployRequest = db.CodeDeployRequests.Find(id);
            if (codeDeployRequest == null) {
                return HttpNotFound();
            }
            return View(codeDeployRequest);
        }

        // POST: CodeDeployRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeDeployRequestID,DBDeployRequestID,User,Team,Environment,AppName,DeployType,JenkinsBuild,DINumber,TFSWorkItems,DeployDateTime,TestableInQA,QAReason,QAMitigation,QAIdentifyFailure,QAFailureImpact,QADataImpact,QAFailureResolution,QATester,SpecialNotes,ConfirmationEmail,SubmissionTime")] CodeDeployRequest codeDeployRequest) {
            if (ModelState.IsValid) {
                db.Entry(codeDeployRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codeDeployRequest);
        }

        // GET: CodeDeployRequests/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeDeployRequest codeDeployRequest = db.CodeDeployRequests.Find(id);
            if (codeDeployRequest == null) {
                return HttpNotFound();
            }
            return View(codeDeployRequest);
        }

        // POST: CodeDeployRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            CodeDeployRequest codeDeployRequest = db.CodeDeployRequests.Find(id);
            db.CodeDeployRequests.Remove(codeDeployRequest);
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
