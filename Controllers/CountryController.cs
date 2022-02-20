using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wjs_c08_react_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace wjs_c08_react_api.Controllers
{
    [ApiController, Route("[controller]/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        
        private DataContext _dataContext;

        public CountryController(ILogger<CountryController> logger, DataContext db)
        {
            _logger = logger;
            _dataContext = db;
        }

        [HttpGet, SwaggerOperation(summary: "Return All Countries", null)]
        public IEnumerable<CountryMedals> Get()
        {
            return _dataContext.Countrys;

        }

        [HttpGet("{id}"), SwaggerOperation(summary: "Return Specific Country", null)]
        public CountryMedals Get(string id)
        {
            return _dataContext.Countrys.Find(id);

        }

        [HttpPost, SwaggerOperation(summary: "Add Country to List", null), ProducesResponseType(typeof(CountryMedals), 201), SwaggerResponse(201, "Created")]
        public CountryMedals Post([FromBody] CountryMedals countryMedals) => _dataContext.AddCountry(new CountryMedals
        {
            Id = countryMedals.Id,
            Name = countryMedals.Name,
            Flag = countryMedals.Flag,
            Bronze = countryMedals.Bronze,
            Gold = countryMedals.Gold,
            Silver = countryMedals.Silver
        });

        [HttpDelete("{id}"), SwaggerOperation(summary: "Remove Country from List", null), ProducesResponseType(typeof(CountryMedals), 204), SwaggerResponse(204, "No Content")]
        public ActionResult Delete(string id)
        {
            CountryMedals country = _dataContext.Countrys.Find(id);
            if(country == null)
            {
                return NotFound();
            }

            _dataContext.DeleteCountry(id);
            return NoContent();
        }
    }
}
