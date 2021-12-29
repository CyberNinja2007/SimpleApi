using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        
        [HttpGet]
        public IEnumerable<Citizen> GetCitizens(int? page, int? minAge, int? maxAge, string sex = "")
        {
            IQueryable<Citizen> citizens;

            if (sex != "")
            {
                citizens = _context.Citizens.Where(citizen => citizen.Sex == sex);
            }
            else
            {
                citizens = _context.Citizens;
            }

            if (minAge != null & maxAge == null)
            {
                citizens = citizens.Where(citizen => citizen.Age > minAge);
            }
            else if (maxAge != null & minAge == null)
            {
                citizens = citizens.Where(citizen => citizen.Age < maxAge);
            }
            else if (maxAge != null & minAge != null)
            {
                citizens = citizens.Where(citizen => citizen.Age > minAge & citizen.Age < maxAge);
            }
            
            int pageNum = page ?? 1;
            int pageSize = 10;
            
            return citizens.Select(citizen => new Citizen{
                Id = citizen.Id,
                Name = citizen.Name,
                Sex = citizen.Sex
            }).ToPagedList(pageNum,pageSize);
        }
        
        [HttpGet("{id}")]
        public async Task<Citizen[]> GetCitizen(int id)
        {
            var citizen = await _context.Citizens.Where(citizen => citizen.Id == id).Select(citizen => new Citizen{
                Name = citizen.Name,
                Sex = citizen.Sex,
                Age = citizen.Age
            }).ToArrayAsync();

            return citizen;
        }
    }
}
