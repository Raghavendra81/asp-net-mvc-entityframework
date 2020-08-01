using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel
{
    [Table("StudentCourse")]
    public class StudentCourse
    {
        [Key]
        public int StudentCourseId { get; set; }

        public string StudentCourseName { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }

        public Course Course { get; set; }
    }
}
