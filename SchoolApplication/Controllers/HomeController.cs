using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationBusinessLogic;
using ApplicationModel;

namespace SchoolApplication.Controllers
{
    public class HomeController : Controller
    {
        private ISchoolBl _iSchoolrepo;
        private SchoolBl _schoolBl;

        public HomeController() : this(SchoolBl.SchoolBusinessLogic()) { }
        public HomeController(ISchoolBl schoolBl)
        {
            _iSchoolrepo = schoolBl;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Admission()
        {
            Admission model = new Admission();

            try
            {
                var businessOperations = _iSchoolrepo;

                model.Course = businessOperations.GetCourses();
                model.Standard = businessOperations.GetStandards();

                Session["Courses"] = model.Course;
                Session["Standard"] = model.Standard;

                if (model.Course.Count <= 0 && model.Standard.Count <= 0)
                {

                    ModelState.AddModelError(string.Empty, "Cannot Process your request at this moment");
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.ToString();
                return View("Error");
            }
            return View(model);
        }

        /// <summary>
        /// post Request
        /// </summary>
        /// <param name="admissions"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Admission(Admission admissions)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    admissions.Course = (List<SelectListItem>)Session["Courses"];
                    admissions.Standard = (List<SelectListItem>)Session["Standard"];

                    return View(admissions);
                }
                else
                {
                    var businessOperations = _iSchoolrepo;
                    int admissionId = businessOperations.RegisterStudent(admissions);

                    if (admissionId > 0)
                    {
                        Session["AdmissionId"] = admissionId;
                        return RedirectToAction("StudentInformation");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Cannot Process your request");
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.ToString();
                return View("Error");
            }

        }

        /// <summary>
        /// Subcourse Dropdown
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public ActionResult GetSubCourses(int course)
        {
            var businessOperations = SchoolBl.SchoolBusinessLogic();

            List<SelectListItem> subCourses = new List<SelectListItem>();

            subCourses = businessOperations.GetSubCourses(course);

            return Json(subCourses, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetStudentData
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetStudentData(int studentId)
        {
            List<Admission> studentList = new List<Admission>();

            var businessOperations = _iSchoolrepo;

            var studentData = businessOperations.GetStudent(studentId);

            if (studentData != null)
            {
                studentList.Add(studentData);
            }
            else
            {
                studentList.Add(null);
            }
            return Json(studentList);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Error()
        {
            return PartialView("Error");
        }

        /// <summary>
        /// StudentInformation
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentInformation()
        {
            var businessOperations = _iSchoolrepo;
            try
            {
                int studentId = (int)Session["AdmissionId"];

                var studentData = businessOperations.GetStudent(studentId);

                return View(studentData);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// AdminLogin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AdminLogin()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.ToString();
                return View("Error");
            }
        }

        /// <summary>
        /// AdminLogin post request
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var businessLogic = SchoolBl.SchoolBusinessLogic();
                    bool validAdmin = businessLogic.VerifyAdmin(admin);

                    if (validAdmin)
                    {
                        Session["AdminId"] = admin.Admin_Id;
                        return RedirectToAction("AdminDashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Check the Credentials");
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {

                return View("Error");
            }
        }

        /// <summary>
        /// Admin Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();

            return View("Index");
        }

        /// <summary>
        /// AdminDashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDashboard()
        {
            if (Session["AdminId"] != null)
            {
                try
                {
                    var businesslogic = SchoolBl.SchoolBusinessLogic();
                    return View(businesslogic.GetStudents());
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.ToString();
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
    }
}