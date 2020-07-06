using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entities;

namespace DataAccess
{
    public class CompanyRepository : IRepository<Company>
    {
        private IList<Company> companies;

        public CompanyRepository()
        {
            companies = new List<Company>
            {
                new Company { Name = "Tieto", IndustryType = IndustryType.IT },
                new Company { Name = "Mahindra", IndustryType = IndustryType.Automobile },
                new Company { Name = "Sun Pharma", IndustryType = IndustryType.Pharma }
        };
        }

        public Company Search(string name)
        {
            return companies.First(c => c.Name.ToUpperInvariant().Equals(name.ToUpperInvariant()));
        }

        public void Add(Company entity)
        {
            this.companies.Add(entity);
        }

        public IEnumerable<Company> GetAll()
        {
            return this.companies;
        }
    }
}
