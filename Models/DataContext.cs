using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

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
        public void PatchCountry(int id, JsonPatchDocument<CountryMedals> patch)
        {
            CountryMedals country = this.Countrys.FirstOrDefault(c => c.Id.Equals(id));
            patch.ApplyTo(country);
            this.SaveChanges();
        }

    }
}
