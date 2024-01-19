using BLL.Interfaces;
using DAL.Context;
using DAL.enities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonGetter : IPersonGetter
    {
        private readonly ApplicationContext _context;

        public PersonGetter( ApplicationContext context)
        {
            _context = context;   
        }

 
        public async Task<IEnumerable<Person>> GetPeople()
        {
            var people = await _context.People.ToListAsync();
            return people;
        }
    }
}
