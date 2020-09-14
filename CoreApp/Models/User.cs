using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid Phone Number")]
        [RegularExpression(@"^([0-9]{10})$",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
