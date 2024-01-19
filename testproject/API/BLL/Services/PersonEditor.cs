using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonEditor:IPersonEditor
    {
        private readonly ApplicationContext _context;

        public PersonEditor(ApplicationContext applicationContext) { 
            _context = applicationContext;      
        }

        public async Task EditInfo(int id, PersonModel personModel)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                throw new NotFoundException("Person to edit is not found");
            }

            // Update properties of the existing person entity with the new values from personModel
            person.Name = personModel.Name;
            person.DateOfBirth = DateTime.Parse(personModel.DateOfBirth.ToString());
            person.IsMarried = personModel.IsMarried;
            person.Phone = personModel.Phone;
            person.Salary = personModel.Salary;

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }

    }
}
