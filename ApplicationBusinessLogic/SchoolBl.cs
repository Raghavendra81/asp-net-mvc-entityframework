using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ApplicationDataAccess;
using ApplicationModel;

namespace ApplicationBusinessLogic
{
    public class SchoolBl : ISchoolBl
    {
        private static IDatabaseOperations _dbContext;

        private static ISchoolBl _iSchoolBl;

        private static SchoolBl _schoolBl;

        private SchoolBl()
        {

        }
        /// <summary>
        /// Singleton Pattern
        /// </summary>
        /// <returns></returns>
        public static SchoolBl SchoolBusinessLogic()
        {
            if (_schoolBl == null)
            {
                _dbContext = new DatabaseOperations();
                return _schoolBl = new SchoolBl();
            }
            else
            {
                return _schoolBl;
            }
        }
        public SchoolBl(IDatabaseOperations repository = null, ISchoolBl schoolBl = null)
        {
            _iSchoolBl = schoolBl;
            _dbContext = repository ?? new DatabaseOperations();
        }
        public SchoolBl(ISchoolBl schoolBl = null)
        {
            _iSchoolBl = schoolBl;
        }

        /// <summary>
        /// List of Courses
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCourses()
        {
            List<SelectListItem> courses;
            try
            {
                courses = _dbContext.GetCourses();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return courses;
        }

        /// <summary>
        /// List of Standards
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStandards()
        {
            List<SelectListItem> standards;
            try
            {
                standards = _dbContext.GetStandards();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return standards;
        }
        /// <summary>
        /// List of Sub courses
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetSubCourses(int courseId)
        {
            List<SelectListItem> subCourses;
            try
            {
                subCourses = _dbContext.GetSubCourses(courseId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return subCourses;
        }

        /// <summary>
        /// New Admission
        /// </summary>
        /// <param name="admission"></param>
        /// <returns></returns>
        public int RegisterStudent(Admission admission)
        {
            int admissionId;
            try
            {
                if (admission == null) return 0;
                admissionId = _dbContext.RegisterStudent(admission);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return admissionId;
        }

        /// <summary>
        /// Returns Student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Admission GetStudent(int studentId)
        {
            Admission studentdata = new Admission();
            try
            {
                studentdata = _dbContext.GetStudent(studentId);

                if (studentdata.AdmissionId == 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return studentdata;
        }

        /// <summary>
        /// Returns Students
        /// </summary>
        /// <returns></returns>
        public List<Admission> GetStudents()
        {
            List<Admission> students = new List<Admission>();

            try
            {
                students = _dbContext.GetStudents();

                if (students.Count == 0) return null;
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
                validAdmin = _dbContext.VerifyAdmin(admin);
            }
            catch (Exception e)
            {
                throw e;
            }

            return validAdmin;
        }
    }
}
