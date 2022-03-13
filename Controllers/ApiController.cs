using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wjs_c08_react_api.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.SignalR;
using wjs_c08_react_api.Hubs;

namespace wjs_c08_react_api.Controllers
{
    [ApiController, Route("[controller]/countries")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        
        private DataContext _dataContext;

        private readonly IHubContext<MedalsHub> _hubContext;
        public ApiController(ILogger<ApiController> logger, DataContext db, IHubContext<MedalsHub> hubContext)
        {
            _logger = logger;
            _dataContext = db;
            _hubContext = hubContext;
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

        [HttpPost, SwaggerOperation(summary: "Add Country to List", null),
         ProducesResponseType(typeof(CountryMedals), 201), SwaggerResponse(201, "Created")]
        public async Task<ActionResult<CountryMedals>> Post([FromBody] CountryMedals countryMedals)
        {

            _dataContext.Add(countryMedals);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveAddMessage", countryMedals);
            this.HttpContext.Response.StatusCode = 201;

            return countryMedals;
        }

        [HttpDelete("{id}"), SwaggerOperation(summary: "Remove Country from List", null), ProducesResponseType(typeof(CountryMedals), 204), SwaggerResponse(204, "No Content")]
        public async Task<ActionResult> Delete(string id)
        {
            CountryMedals countryMedals = await _dataContext.Countrys.FindAsync(id);
            if(countryMedals == null)
            {
                return NotFound();
            }

            _dataContext.Remove(countryMedals);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveDeleteMessage", id);
            return NoContent();
        }

        [HttpPatch("{id}"), SwaggerOperation(summary: "update member from collection", null), ProducesResponseType(typeof(CountryMedals), 204), SwaggerResponse(204, "No Content")]
        // update country (specific fields)
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountryMedals> patch)
        {
            CountryMedals countryMedals = await _dataContext.Countrys.FindAsync(id);
            if (countryMedals == null)
            {
                return NotFound();
            }
            patch.ApplyTo(countryMedals);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceivePatchMessage", countryMedals);
            return NoContent();
        }
    }
}
