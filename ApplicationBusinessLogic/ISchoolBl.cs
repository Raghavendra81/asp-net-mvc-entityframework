using System.Collections.Generic;
using System.Web.Mvc;
using ApplicationModel;

namespace ApplicationBusinessLogic
{
    public interface ISchoolBl
    {
        List<SelectListItem> GetCourses();
        List<SelectListItem> GetStandards();
        Admission GetStudent(int studentId);
        List<Admission> GetStudents();
        List<SelectListItem> GetSubCourses(int courseId);
        int RegisterStudent(Admission admission);
        bool VerifyAdmin(Admin admin);
    }
}