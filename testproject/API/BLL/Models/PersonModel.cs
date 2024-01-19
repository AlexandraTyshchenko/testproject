using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PersonModel
    {
        public string Name { get; set; }


        public DateOnly DateOfBirth { get; set; }


        public bool IsMarried { get; set; }


        public string Phone { get; set; }


        public decimal Salary { get; set; }
    }
}
