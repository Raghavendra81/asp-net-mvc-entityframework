using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ApplicationBusinessLogic;
using ApplicationDataAccess;
using Moq;
using NUnit.Framework;
using SchoolApplication;
using SchoolApplication.Controllers;
using ApplicationModel;

namespace SchoolApplication.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private Mock<ISchoolBl> _repository;

        private Mock<IDatabaseOperations> _data;

        Mock<HomeController> controller;

        HomeController _controller;

        private SchoolBl _schoolBl;
        [SetUp]
        public void Setup()
        {
            _data = new Mock<IDatabaseOperations>();

            _repository = new Mock<ISchoolBl>();

            _schoolBl = new SchoolBl(_data.Object, _repository.Object);

            controller = new Mock<HomeController>(_repository.Object);

            _controller = new HomeController();
        }

        [Test]
        public void Index()
        {

            ViewResult result = _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void LoadStudentRegistrationPage_Scenario_Returns_Courses()
        {
            var result = _controller.Admission();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_NewStudent_Scenario_ModelStateTrue()
        {
            var result = _controller.Admission(new Admission
            {
                Address = "Telangana",
                ContactNumber = "8978740498",
                FirstName = "Raghavendra",
                FatherName = "Ram Gopal",
                LastName = "Chilukuri",
                DateOfBirth = DateTime.Parse("16-06-1996"),
                CourseId = 1,
                StudentCourseId = 3,
                StandardId = 10,
                Gender = "Male",

            }) as ViewResult;

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void AdminLogin_page()
        {

            ViewResult result = _controller.AdminLogin() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void verifyAdmin_adminFound_Returns_True()
        {
            var result = _controller.AdminLogin(new Admin
            {
                Admin_Id = "admin",
                Password = "correctPassword"
            }) as ViewResult;

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void LoadStudentSubCoursesBy_WithouCourseId()
        {
            ViewResult result = _controller.GetSubCourses(1) as ViewResult;
            Assert.That(result, Is.Null);
        }
    }
}
