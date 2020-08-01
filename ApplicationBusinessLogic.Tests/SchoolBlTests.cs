using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ApplicationDataAccess;
using ApplicationModel;
using Moq;
using NUnit.Framework;
namespace ApplicationBusinessLogic.Tests
{
    [TestFixture]
    public class SchoolBlTests
    {
        private Mock<IDatabaseOperations> _repository;

        private SchoolBl _schoolBl;
        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IDatabaseOperations>();
            _schoolBl = new SchoolBl(_repository.Object);
        }
        [Test]
        public void GetCourses_NoCoursesFound_Returns_EmptySelectListItems()
        {
            _repository.Setup(r => r.GetCourses()).Returns(new List<SelectListItem>());

            var result = _schoolBl.GetCourses();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetCourses_CoursesFound_Returns_ListOfSelectListItems()
        {
            _repository.Setup(r => r.GetCourses()).Returns(new List<SelectListItem>()
            {
                new SelectListItem(){Text = "Physics" ,Value = "1"},
                new SelectListItem(){Text = "Mathematics",Value = "2"},
                new SelectListItem(){Text = "Biology",Value = "2"}
            });

            var result = _schoolBl.GetCourses();

            Assert.That(result, Is.TypeOf<List<SelectListItem>>());
        }
        [Test]
        public void GetStandards_NoStandardsFound_Returns_EmptySelectListItems()
        {
            _repository.Setup(r => r.GetStandards()).Returns(new List<SelectListItem>());

            var result = _schoolBl.GetStandards();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetStandards_StandardsFound_Returns_ListOfSelectListItems()
        {
            _repository.Setup(r => r.GetStandards()).Returns(new List<SelectListItem>()
            {
                new SelectListItem(){Text = "Grade 1" ,Value = "1"},
                new SelectListItem(){Text = "Grade 2",Value = "2"},
                new SelectListItem(){Text = "Grade 3",Value = "2"}
            });

            var result = _schoolBl.GetStandards();

            Assert.That(result, Is.TypeOf<List<SelectListItem>>());
        }

        [Test]
        public void GetStudents_StudentsNotFound_Returns_Empty()
        {
            _repository.Setup(r => r.GetStudents()).Returns(new List<Admission>());

            var result = _schoolBl.GetStudents();

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetStudents_StudentsFound_ReturnsListOfStudents()
        {
            _repository.Setup(r => r.GetStudents()).Returns(admissions);

            var result = _schoolBl.GetStudents();

            Assert.That(result, Is.TypeOf<List<Admission>>());
        }

        [Test]
        public void GetStudentsById_StudentNotFound_Returns_Empty()
        {
            _repository.Setup(r => r.GetStudent(1)).Returns(new Admission());

            var result = _schoolBl.GetStudent(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetStudentsById_StudentFound_Returns_Student()
        {
            _repository.Setup(r => r.GetStudent(1)).Returns(admissions[0]);

            var result = _schoolBl.GetStudent(1);

            Assert.That(result, Is.TypeOf<Admission>());
        }
        [Test]
        public void GetSubCourses_ArgumentExceptio_Returns_Empty()
        {
            _repository.Setup(r => r.GetSubCourses(0)).Returns(new List<SelectListItem>());

            var result = _schoolBl.GetSubCourses(0);

            Assert.That(result, Is.EquivalentTo(""));

        }

        [Test]
        public void GetSubCourses_SubCoursesNotFound_Returns_Empty()
        {
            _repository.Setup(r => r.GetSubCourses(1)).Returns(new List<SelectListItem>());

            var result = _schoolBl.GetSubCourses(1);

            Assert.That(result, Is.EquivalentTo(""));
        }

        [Test]
        public void GetSubCourses_SubCoursesFound_ReturnsSubCourses()
        {
            _repository.Setup(r => r.GetSubCourses(1)).Returns(new List<SelectListItem>()
            {
                new SelectListItem(){Text = "Theromdynamics" ,Value = "1"},
                new SelectListItem(){Text = "Magnetism",Value = "2"},
                new SelectListItem(){Text = "Quantum Physics",Value = "3"}
            });

            var result = _schoolBl.GetSubCourses(1);

            Assert.That(result, Is.TypeOf<List<SelectListItem>>());
        }

        [Test]
        public void verifyAdmin_adminNotFound_Returns_False()
        {
            var admin = new Admin()
            { Admin_Id = "admin", Password = "wrongpasswordadmin" };

            _repository.Setup(r => r.VerifyAdmin(admin)).Returns(false);

            var result = _schoolBl.VerifyAdmin(admin);

            Assert.That(result, Is.False);
        }

        [Test]
        public void verifyAdmin_adminFound_Returns_True()
        {
            var admin = new Admin()
            { Admin_Id = "admin", Password = "correctPassword" };

            _repository.Setup(r => r.VerifyAdmin(admin)).Returns(true);

            var result = _schoolBl.VerifyAdmin(admin);

            Assert.That(result, Is.True);
        }

        [Test]
        public void InsertNewAdmission_Scenario_modelisNull_ReturnsZero()
        {
            _repository.Setup(r => r.RegisterStudent(null)).Returns(0);

            var result = _schoolBl.RegisterStudent(new Admission());

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void InsertNewAdmission_Scenario_modelIsValid_ReturnsStudentID()
        {
            _repository.Setup(r => r.RegisterStudent(admission)).Returns(1);

            var result = _schoolBl.RegisterStudent(admission);

            Assert.That(result, Is.TypeOf<int>());
        }

        private Admission admission = new Admission
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
        };

        private List<Admission> admissions = new List<Admission>()
        {
            new Admission()
            {
                Address = "Telangana",
                AdmissionId = 1,
                ContactNumber = "8978740498",
                FirstName = "Raghavendra",
                FatherName = "Ram Gopal",
                LastName = "Chilukuri",
                CourseName = "Physics",
                SubCourseName = "Thermodynamics",
                Grade = "Grade 10",
                Course = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text = "Physics",
                        Value = "1"
                    }
                },
                StudentCourse = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text = "Thermodynamics",
                        Value = "1"
                    }
                },
                Standard = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text = "Grade 10",
                        Value = "1"
                    }
                }
            }
        };
    }
}
