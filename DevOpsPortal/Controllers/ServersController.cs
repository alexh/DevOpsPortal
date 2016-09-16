using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers
{
    public class ServersController : BaseController
    {
        private ServerDBContext db = new ServerDBContext();

        // GET: Servers
        public override ActionResult Index()
        {
            ViewBag.PageName = "Servers";
            base.Index();
            return View();
        }

        public ActionResult Tables()
        {
            return View(ServerManager.GetServers());
        }

        public JsonResult GetServers()
        {
            return Json(ServerManager.GetServers(), JsonRequestBehavior.AllowGet);
        }

        // GET: Servers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Server server = ServerManager.GetServer(id.Value);
            if (server == null)
            {
                return View("../Shared/NotFound");
            }
            return View(server);
        }

        // GET: Servers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServerId,ComputerName,IPAddress,SerialNumber,Make,Model,OS,TotalMemory,CPUSockets,CPUCoresPerSocket,Drives,ProbeSiteName,DateImported,LastUpdated")] Server server)
        {
            if (ModelState.IsValid)
            {
                ServerManager.CreateServer(server);
                return RedirectToAction("Index");
            }

            return View(server);
        }

        // GET: Servers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Server server = ServerManager.GetServer(id.Value);
            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // POST: Servers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServerId,ComputerName,IPAddress,SerialNumber,Make,Model,OS,TotalMemory,CPUSockets,CPUCoresPerSocket,Drives,ProbeSiteName,DateImported,LastUpdated")] Server server)
        {
            if (ModelState.IsValid)
            {
                ServerManager.UpdateServer(server);
                return RedirectToAction("Index");
            }
            return View(server);
        }

        // GET: Servers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Server server = ServerManager.GetServer(id.Value);
            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // POST: Servers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServerManager.DeleteServer(id);
            return RedirectToAction("Index");
        }
    }
}