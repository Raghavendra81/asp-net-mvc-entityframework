using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ApplicationModel
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }

    }
}
