using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel
{
    [Table("TblAdmin")]
    public class Admin
    {
        [Required(ErrorMessage = "Enter Admin Id")]
        [Key]
        public string Admin_Id { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}
