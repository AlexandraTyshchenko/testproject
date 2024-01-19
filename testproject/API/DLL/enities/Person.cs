using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.enities
{
 public class Person
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }

    public bool IsMarried { get; set; }

    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
    public string Phone { get; set; }

    public decimal Salary { get; set; }
}

}
