using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Data;
using SimpleApi.Models;
using X.PagedList;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly CitizenContext _context;

        public CitizenController(CitizenContext context)
        {
            _context = context;
        }

        // GET: api/Citizen
        [HttpGet]
        public IEnumerable<Citizen> GetCitizens(int? page,string sex = "")
        {
            IEnumerable<Citizen> citizens;
            
            if(sex != "")
            {
                citizens = _context.Citizens.Where(citizen => citizen.Sex == sex);
            }
            else
            {
                citizens = _context.Citizens;
            }
            
            int pageNum = page ?? 1;
            int pageSize = 10;
            
            return citizens.ToArray();
        }

        // GET: api/Citizen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            return citizen;
        }
    }
}
