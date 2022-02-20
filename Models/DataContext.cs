using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wjs_c08_react_api.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<CountryMedals> Countrys { get; set; }

        public CountryMedals AddCountry(CountryMedals countryMedals)
        {
            this.Add(countryMedals);
            this.SaveChanges();
            return countryMedals;
        }

        public void DeleteCountry(string id)
        {
            this.Remove(this.Countrys.FirstOrDefault(c => c.Id.Equals(id)));
            this.SaveChanges();
        }
    }
}
