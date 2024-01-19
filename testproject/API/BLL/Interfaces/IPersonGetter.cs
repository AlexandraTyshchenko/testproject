using DAL.enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPersonGetter
    {
        Task<IEnumerable<Person>> GetPeople();
    }
}
