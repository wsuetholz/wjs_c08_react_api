using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wjs_c08_react_api.Models
{
    public class CountryMedals
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Flag { get; set; }
        public int Bronze { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
    }
}
