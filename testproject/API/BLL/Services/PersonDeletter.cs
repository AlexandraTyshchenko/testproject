using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonDeletter : IPersonDeletter
    {
        private readonly ApplicationContext _context;

        public PersonDeletter(ApplicationContext context)
        {
              _context = context;
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                throw new NotFoundException();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

        }
    }
}
