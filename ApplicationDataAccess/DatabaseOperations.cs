using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ApplicationModel;

namespace ApplicationDataAccess
{
    public interface IDatabaseOperations
    {
        List<SelectListItem> GetCourses();
        List<SelectListItem> GetStandards();
        List<SelectListItem> GetSubCourses(int courseId);
        int RegisterStudent(Admission admission);
        Admission GetStudent(int admissionId);
        List<Admission> GetStudents();
        bool VerifyAdmin(Admin admin);
    }

    public class DatabaseOperations : IDatabaseOperations
    {
        private SchoolDataAccess _schoolDataAccess;

        public DatabaseOperations()
        {
            _schoolDataAccess = new SchoolDataAccess();
        }
        /// <summary>
        /// Returns List of Courses
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCourses()
        {

            List<SelectListItem> courses;
            try
            {
                courses = _schoolDataAccess.Courses.Select(x =>
                    new SelectListItem() { Text = x.CourseName, Value = x.CourseId.ToString() }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return courses;
        }

        /// <summary>
        /// Returns List of Standards
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStandards()
        {

            List<SelectListItem> standards;
            try
            {
                standards = _schoolDataAccess.Standards.Select(x =>
                    new SelectListItem() { Text = x.Grade, Value = x.StandardId.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return standards;
        }

        /// <summary>
        /// Returns List of SubCourses
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetSubCourses(int courseId)
        {

            List<SelectListItem> courses;
            try
            {
                courses = _schoolDataAccess.StudentCourses.Where(x => x.CourseId == courseId).Select(x =>
                       new SelectListItem() { Text = x.StudentCourseName, Value = x.StudentCourseId.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return courses;
        }

        /// <summary>
        /// New admission
        /// </summary>
        /// <param name="admission"></param>
        /// <returns></returns>
        public int RegisterStudent(Admission admission)
        {
            int admissionId;
            try
            {
                _schoolDataAccess.Admissions.Add(admission);
                _schoolDataAccess.SaveChanges();

                admissionId = admission.AdmissionId;

            }
            catch (Exception e)
            {
                throw e;
            }

            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw e;
            //}

            return admissionId;
        }

        /// <summary>
        /// Returns a student
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        public Admission GetStudent(int admissionId)
        {
            Admission admissionDetails = new Admission();
            try
            {
                var studentDetails = (from admissions in _schoolDataAccess.Admissions
                                      join course in _schoolDataAccess.Courses on admissions.CourseId equals course.CourseId
                                      join studentcourse in _schoolDataAccess.StudentCourses on admissions.StudentCourseId equals
                                          studentcourse.StudentCourseId
                                      join grade in _schoolDataAccess.Standards on admissions.StandardId equals grade.StandardId
                                      where admissions.AdmissionId == admissionId
                                      select new
                                      {
                                          admissions.AdmissionId,
                                          admissions.FirstName,
                                          admissions.FatherName,
                                          grade.Grade,
                                          course.CourseName,
                                          studentcourse.StudentCourseName
                                      }).Distinct().ToList();

                foreach (var detail in studentDetails)
                {
                    admissionDetails.AdmissionId = detail.AdmissionId;
                    admissionDetails.FirstName = detail.FirstName;
                    admissionDetails.FatherName = detail.FatherName;
                    admissionDetails.CourseName = detail.CourseName;
                    admissionDetails.SubCourseName = detail.StudentCourseName;
                    admissionDetails.Grade = detail.Grade;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return admissionDetails;
        }

        /// <summary>
        /// Returns List of students
        /// </summary>
        /// <returns></returns>
        public List<Admission> GetStudents()
        {
            List<Admission> students = new List<Admission>();
            try
            {
                var studentList = _schoolDataAccess.Admissions.Include(m => m.TblCourse).Include(m => m.TblStandard)
                    .Include(m => m.TblStudentCourse).Distinct().ToList();

                foreach (var data in studentList)
                {
                    data.CourseName = data.TblCourse.CourseName;
                    data.Grade = data.TblStandard.Grade;
                    data.SubCourseName = data.TblStudentCourse.StudentCourseName;

                    students.Add(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return students;
        }

        /// <summary>
        /// Verifies Admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool VerifyAdmin(Admin admin)
        {
            bool validAdmin = false;
            try
            {
                var admindata = _schoolDataAccess.Admins
                    .Where(m => m.Admin_Id == admin.Password && m.Password == admin.Password).FirstOrDefault();

                if (admindata != null)
                {
                    validAdmin = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return validAdmin;
        }
    }
}
