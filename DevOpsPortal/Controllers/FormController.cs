using DevOpsPortal.Managers;
using DevOpsPortal.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace DevOpsPortal.Controllers {
    public class FormController : BaseController {
        private CodeDeployRequestDbContext CodeDb = new CodeDeployRequestDbContext();
        private DBDeployDbContext DBDb = new DBDeployDbContext();
        GeneralRequestDbContext gdbc = new GeneralRequestDbContext();

        public ActionResult DbDeployRequestPartialForFull() {
            return View();
        }

        public override ActionResult Index() {
            if (!this.CanDeploy(this.GetMyUser())) {
                return RedirectToAction("Denied");
            }
            return base.Index();
        }

        // GET: Form/General ONLY ALLOWED WHEN CALLED AS PARTIAL
        public ActionResult General() {
            GeneralRequestViewModel model = new GeneralRequestViewModel();
            model.User = this.PrepareRequestPage();
            ViewBag.PageName = "GeneralForm";
            this.SetUsername();
            SetUserTeam();

            return PartialView(model);
        }

        // GET: Form/CICD
        public ActionResult CICD() {
            ViewBag.PageName = "CICD";
            SetUserTeam();
            return View();
        }

        // GET: Form/Success
        //Used when a form is submitted successfully and is saved in the DevOps Database
        public ActionResult Success() {

            SetUserTeam();
            return View();
        }

        // GET: Form/Denied
        //Used when a user tries to access a form they do not have access to
        public ActionResult Denied() {
            SetUserTeam();
            return View();
        }

        // GET: Form/FormSelect
        //Main view used to select form and displays that form as a partial view. Only view that is accessed to fill out a form.
        public ActionResult FormSelect() {
            if (!this.CanDeploy(this.GetMyUser())) {
                return RedirectToAction("Denied");
            }
            ViewBag.PageName = "Forms";
            FormSelectViewModel model = new FormSelectViewModel();

            User user = this.GetMyUser();
            model.CanAccessDeploy = this.CanDeploy(user);
            SetUserTeam();

            return View(model);
        }

        // GET: Form/CodeDeployRequest 
        // Deployment (App Only)
        public ActionResult CodeDeployRequest() {
            //validate user
            if (!this.CanDeploy(this.GetMyUser())) {
                return RedirectToAction("Denied");
            }

            CodeDeployRequestViewModel model = new CodeDeployRequestViewModel();
            model.TestableInQABool = true;
            model.TestableInQTSBool = true;
            model.User = this.PrepareRequestPage();

            return PartialView(model);
        }

        // GET: Form/DbDeployRequest
        // Deployment (SQL Only)
        public ActionResult DbDeployRequest() {
            //validate user
            if (!this.CanDeploy(this.GetMyUser())) {
                return RedirectToAction("Denied");
            }

            DBDeployRequestViewModel model = new DBDeployRequestViewModel();
            model.TestableInQABool = true;
            model.TestableInQTSBool = true;
            model.DeploymentCycle = "Continuous";
            model.User = this.PrepareRequestPage();

            return PartialView(model);
        }

        // GET: Form/FullRequest
        // Deployment (App + SQL)
        public ActionResult FullRequest() {
            //validate user
            if (!this.CanDeploy(this.GetMyUser())) {
                return RedirectToAction("Denied");
            }

            FullRequestViewModel model = new FullRequestViewModel();
            model.TestableInQABoolDB = true;
            model.TestableInQABool = true;
            model.TestableInQTSBool = true;
            model.TestableInQTSBoolDB = true;
            String userID = this.PrepareRequestPage();
            model.User = userID;

            return PartialView(model);
        }

        //Checks if current user should have access to the deployment forms
        private bool CanDeploy(User user) {
            string User = user.User_ID.ToString().TrimEnd();
            TeamDbContext tc = new TeamDbContext();


            //Full control for DevOps
            //CHANGE WHEN FORMS GO LIVE -- currently gives DevOps access and blocks everyone else. Remove the else statement to give all access
            if (user.Team.TrimEnd() == "5") {
                return true;
            } else {
                return false;
            }


            var team = tc.MasterTeamInventories.AsEnumerable();


            foreach (Team t in team) {
                //Checks if current user is a team lead
                if (t.TeamLead != null && t.TeamLead.ToString().Equals(User)) {
                    return true;
                }
                //Checks if current user is a QA person
                string[] qa = t.QA.Split(',');
                foreach (string q in qa) {
                    if (q.TrimEnd().Equals(User)) {
                        return true;
                    }
                }
            }
            return false;
        }


        // returns user ID as string -- Used to set the Model.User in Forms
        private String PrepareRequestPage() {
            User user = this.GetMyUser();
            ViewBag.UserDisplayName = user.FirstName.TrimEnd() + " " + user.LastName.TrimEnd();
            GenerateTeamSelectList(user);
            return user.User_ID.ToString();
        }



        //Generates the EnvironmentSelectList for some Forms
        private void GenerateEnvSelectList() {
            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Selected = true, Text = "Select Environment", Value = "-1" });
            var envSelectList = new SelectList(listItems, "Value", "Text");
            ViewBag.EnvSelectList = envSelectList;
        }

        // Adds the team select list to the ViewBag -- Only adds teams that the current user is a part of according to MasterUserInventory
        private void GenerateTeamSelectList(User user) {
            string teams = user.Team.TrimEnd();

            //Lets DevOps users enter Request for any team -- change if neccessary
            if (teams == "5") {
                teams = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19";
            }
            string[] teamsArr = teams.Split(',');
            List<string> teamNames = new List<string>();


            foreach (string id in teamsArr) {
                teamNames.Add(TeamManager.GetTeamName(Convert.ToInt32(id)));
            }


            var listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Selected = true, Text = "Select Team", Value = "-1" });
            for (int i = 0; i < teamsArr.Length; i++) {
                // !!!!! Change Value to same as Text if we want Team field to be name and not ID !!!!!
                listItems.Add(new SelectListItem { Selected = false, Text = teamNames[i], Value = teamNames[i] });
            }
            var teamSelectList = new SelectList(listItems, "Value", "Text");
            ViewBag.TeamSelectList = teamSelectList;
        }


        //Populates the data needed to show the form views
        private void PopulateViewData() {
            GenerateTeamSelectList(this.SetUsername());
        }

        //Sets the user's name in the ViewBag to the current user's name
        private User SetUsername() {
            User user = this.GetMyUser();
            ViewBag.UserDisplayName = user.FirstName.TrimEnd() + " " + user.LastName.TrimEnd();
            return user;
        }

        //Tests if the it is testable that the fields aren't null
        //True means the tests passed, false means the tests failed
        private bool TestQAFields(CodeDeployRequestViewModel model) {
            if (!model.TestableInQABool) {
                if (model.QATester == null ||
                    model.QADataImpact == null ||
                    model.QAFailureImpact == null ||
                    model.QAFailureResolution == null ||
                    model.QAIdentifyFailure == null ||
                    model.QAMitigation == null ||
                    model.QAReason == null) {
                    //Repopulate View Data
                    this.PopulateViewData();
                    //Done repopulating
                    ViewBag.QAError = true;

                    return false;
                }
            }
            return true;
        }

        private bool TestQAFields(DBDeployRequestViewModel model) {
            if (!model.TestableInQABool) {
                if (model.QATester == null ||
                    model.QADataImpact == null ||
                    model.QAFailureImpact == null ||
                    model.QAFailureResolution == null ||
                    model.QAIdentifyFailure == null ||
                    model.QAMitigation == null ||
                    model.QAReason == null) {
                    //Repopulate View Data
                    this.PopulateViewData();
                    //Done repopulating
                    ViewBag.QAError = true;

                    return false;
                }
            }
            return true;
        }

        //Tests model and then adds it to the database
        private AddDBDeployRequestReturn AddDBDeployRequest(DBDeployRequestViewModel model) {
            if (ModelState.IsValid) {
                if (!this.TestQAFields(model)) {
                    //Repopulate View Data
                    this.PopulateViewData();
                    //Done repopulating
                    ViewBag.QAError = true;

                    return new AddDBDeployRequestReturn(model, false);
                }

                model.SubmissionTime = DateTime.Now;

                AddDBDeployRequestReturn toReturnTrue = new AddDBDeployRequestReturn(model, true);
                AddDBDeployRequestReturn toReturnFalse = new AddDBDeployRequestReturn(model, false);

                //create DBDeployRequestObject
                DBDeployRequest request = new DBDeployRequest(model);
                try {
                    //Add form to DB
                    DBDb.DBDeployRequests.Add(request);
                    DBDb.SaveChanges();
                }
                catch {
                    //Repopulate View Data
                    this.PopulateViewData();
                    //Done repopulating

                    return toReturnFalse;
                }
                int DBId = request.DBDeployRequestID;
                return toReturnTrue;
            } else {
                //Repopulate View Data
                this.PopulateViewData();
                //Done repopulating
                AddDBDeployRequestReturn toReturnFalse = new AddDBDeployRequestReturn(model, false);
                return toReturnFalse;
            }
        }

        //Tests model and then adds it to the database
        private AddCodeDeployRequestReturn AddCodeDeployRequest(CodeDeployRequestViewModel model) {
            if (ModelState.IsValid) {
                if (!this.TestQAFields(model)) {
                    //Repopulate View Data
                    this.PopulateViewData();
                    //Done repopulating
                    ViewBag.QAError = true;

                    return new AddCodeDeployRequestReturn(model, false);
                }
                //Process data

                // merge date and time fields
                var datetime = model.DeployDateTimeNotNull;
                if (model.DeployTime.Contains("PM")) {
                    datetime = datetime.AddHours(12);
                }
                int numHours = Int32.Parse(model.DeployTime.ElementAt(0).ToString());
                datetime = datetime.AddHours(numHours);
                model.DeployDateTime = datetime;

                model.SubmissionTime = DateTime.Now;

                AddCodeDeployRequestReturn toReturnTrue = new AddCodeDeployRequestReturn(model, true);
                AddCodeDeployRequestReturn toReturnFalse = new AddCodeDeployRequestReturn(model, false);

                //create CodeDeployRequestObject
                CodeDeployRequest request = new CodeDeployRequest(model);

                try {
                    //Add form to DB
                    CodeDb.CodeDeployRequests.Add(request);
                    CodeDb.SaveChanges();
                }
                catch {
                    //Repopulate View Data
                    this.PopulateViewData();
                    //Done repopulating

                    return toReturnFalse;
                }
                int CodeId = request.CodeDeployRequestID;
                return toReturnTrue;
            } else {
                //Repopulate View Data
                this.PopulateViewData();
                //Done repopulating
                AddCodeDeployRequestReturn toReturnFalse = new AddCodeDeployRequestReturn(model, false);
                return toReturnFalse;
            }
        }

        // POST Form/CodeDeployRequest
        [HttpPost]
        public ActionResult CodeDeployRequest(CodeDeployRequestViewModel model) {
            model.DeployType = "Code";

            AddCodeDeployRequestReturn returned = this.AddCodeDeployRequest(model);
            bool successful = returned.successful;
            if (successful) {
                return RedirectToAction("Success");
            } else {
                return View(model);
            }
        }

        // POST Form/DBDeployRequest
        [HttpPost]
        public ActionResult DBDeployRequest(DBDeployRequestViewModel model) {
            AddDBDeployRequestReturn returned = this.AddDBDeployRequest(model);
            bool successful = returned.successful;
            if (successful) {
                return RedirectToAction("Success");
            } else {
                return View(model);
            }
        }

        // POST Form/FullDeployRequest
        [HttpPost]
        public ActionResult FullRequest(FullRequestViewModel model) {
            model.DeployType = "Code and DB";
            String user = model.User;
            CodeDeployRequestViewModel code = new CodeDeployRequestViewModel(model);
            DBDeployRequestViewModel db = new DBDeployRequestViewModel(model);
            var codeReturn = this.AddCodeDeployRequest(code);
            if (codeReturn.successful) {
                //adding ID to DB request
                var requests = from m in CodeDb.CodeDeployRequests
                               where (m.User == user)
                               orderby m.SubmissionTime descending
                               select m.CodeDeployRequestID;
                var codeId = requests.First();
                db.CodeDeployRequestID = codeId;
            } else {
                return View(model);
            }

            var dbReturn = this.AddDBDeployRequest(db);
            if (dbReturn.successful) {
            } else {
                return View(model);
            }

            if (codeReturn.successful && dbReturn.successful) {
                //Update ID of code request
                var requests = from m in DBDb.DBDeployRequests
                               where (m.User == user)
                               orderby m.SubmissionTime descending
                               select m.DBDeployRequestID;
                var DBId = requests.First();
                var toUpdates = from m in CodeDb.CodeDeployRequests
                                where (m.User == user)
                                orderby m.SubmissionTime descending
                                select m;
                toUpdates.First().DBDeployRequestID = DBId;

                try {
                    CodeDb.SaveChanges();
                }
                catch {
                }
                return RedirectToAction("Success");
            } else {
                return View(model);
            }
        }

        // POST Form/General
        [HttpPost]
        public ActionResult General(GeneralRequestViewModel model) {
            if (ModelState.IsValid) {
                //Model is valid
                var Image = ConvertToBytes(model.Attachment);
                GeneralRequest request = new GeneralRequest(model, Image);

                try {
                    gdbc.GeneralRequests.Add(request);
                    gdbc.SaveChanges();
                }
                catch {
                    //Repopulate View Data
                    this.PopulateViewData();
                    return View(model);
                }
                return RedirectToAction("Success");

            }
            //Repopulate View Data
            this.PopulateViewData();
            return View(model);
        }



        //Converts the HttpPostedFileBase to a byte[]
        //Used to put images in the database
        public byte[] ConvertToBytes(HttpPostedFileBase image) {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int) image.ContentLength);
            return imageBytes;
        }

        //Gets the teams of the current user
        public string GetMyTeams() {
            return TeamManager.GetMyTeams(User.Identity.Name);
        }

        // Returns current user's user object
        public Models.User GetMyUser() {
            return UserManager.GetMyUser(User.Identity.Name);
        }

    }
}