using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ApplicationModel
{
    [Table("Student")]
    public class Admission
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdmissionId { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Enter a valid name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Enter a valid name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Date Of Birth  is Required")]
        //[RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])-(0[1-9]|1[0-2])-((19|20)\d\d))$")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Father Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Enter a valid name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"[0-9]{10}")]
        public string ContactNumber { get; set; }

        public Course TblCourse { get; set; }

        [ForeignKey("TblCourse")]
        [Required]
        public int? CourseId { get; set; }

        [NotMapped]
        public string CourseName { get; set; }
        [NotMapped]
        public List<SelectListItem> Course { get; set; }


        public Standard TblStandard { get; set; }

        [ForeignKey("TblStandard")]
        [Required]
        public int? StandardId { get; set; }

        [NotMapped]
        public string Grade { get; set; }

        [NotMapped]
        public List<SelectListItem> Standard { get; set; }

        public StudentCourse TblStudentCourse { get; set; }

        [ForeignKey("TblStudentCourse")]
        [Required]
        public int? StudentCourseId { get; set; }

        [NotMapped]
        public string SubCourseName { get; set; }
        [DisplayName("Student Course")]
        [NotMapped]
        public List<SelectListItem> StudentCourse { get; set; }
    }
}
