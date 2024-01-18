﻿
namespace Api.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public bool IsMarried { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }
    }
}

