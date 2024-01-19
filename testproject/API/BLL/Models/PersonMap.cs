using DAL.enities;

using CsvHelper.Configuration;

namespace BLL.Models
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Name);
            Map(m => m.Phone);
            Map(m => m.IsMarried);
            Map(m => m.Salary);
            Map(m => m.DateOfBirth);
        }
    }
}
