using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using wjs_c08_react_api.Hubs;
using wjs_c08_react_api.Models;


namespace wjs_c08_react_api.Controllers
{
    [Authorize]
    [ApiController, Route("jwt/api/country")]
    public class JwtApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private DataContext _dataContext;

        private readonly IHubContext<MedalsHub> _hubContext;
        public JwtApiController(ILogger<ApiController> logger, DataContext db, IHubContext<MedalsHub> hubContext)
        {
            _dataContext = db;
            _logger = logger;
            _hubContext = hubContext;
        }

        // http get entire collection
        [HttpGet, SwaggerOperation(summary: "return entire collection", null)]
        public IEnumerable<CountryMedals> Get()
        {
            return _dataContext.Countrys;
        }

        // http get specific member of collection
        [HttpGet("{id}"), SwaggerOperation(summary: "return specific member of collection", null)]
        public CountryMedals Get(int id)
        {
            return _dataContext.Countrys.Find(id);
        }

        // http post member to collection
        [HttpPost, SwaggerOperation(summary: "add member to collection", null), ProducesResponseType(typeof(CountryMedals), 201), SwaggerResponse(201, "Created")]
        [Authorize(Roles = "medals-post")]
        public async Task<ActionResult<CountryMedals>> Post([FromBody] CountryMedals country)
        {
            _dataContext.Add(country);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveAddMessage", country);
            this.HttpContext.Response.StatusCode = 201;
            return country;
        }

        // http delete member from collection
        [HttpDelete("{id}"), SwaggerOperation(summary: "delete member from collection", null), ProducesResponseType(typeof(CountryMedals), 204), SwaggerResponse(204, "No Content")]
        [Authorize(Roles = "medals-delete")]
        public async Task<ActionResult> Delete(int id)
        {
            CountryMedals country = await _dataContext.Countrys.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _dataContext.Remove(country);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveDeleteMessage", id);
            return NoContent();
        }

        // http patch member of collection
        [HttpPatch("{id}"), SwaggerOperation(summary: "update member from collection", null), ProducesResponseType(typeof(CountryMedals), 204), SwaggerResponse(204, "No Content")]
        [Authorize(Roles = "medals-patch")]
        // update country (specific fields)
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CountryMedals> patch)
        {
            CountryMedals country = await _dataContext.Countrys.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            patch.ApplyTo(country);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceivePatchMessage", country);
            return NoContent();
        }
    }
}
