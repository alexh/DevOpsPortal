using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers
{
    public class BaseController : Controller
    {
        public User CurrentUser;

        public BaseController()
        {
        }

        public virtual ActionResult Index()
        {
            if (User != null)
            {
                if (User.Identity.IsAuthenticated && ViewBag.UserTeam == null)
                {
                    SetUserTeam();
                }
            }
            return View();
        }

        public void SetUserTeam()
        {
            string team = TeamManager.GetMyTeams(User.Identity.Name);
            if (team != null)
            {
                team = team.TrimEnd();
                ViewBag.UserTeam = team;
            }
        }
    }
}