using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel
{
    [Table("Standard")]
    public class Standard
    {
        [Key]
        public int StandardId { get; set; }

        public string Grade { get; set; }
    }
}
