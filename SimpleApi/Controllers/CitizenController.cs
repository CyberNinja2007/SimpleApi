using System.Collections.Generic;
using System.Linq;
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
        
        [HttpGet]
        public IEnumerable<Citizen> GetCitizens(int? page, int minAge = -1, int maxAge = -1, string sex = "")
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

            if (minAge != -1 & maxAge == -1)
            {
                citizens = citizens.Where(citizen => citizen.Age > minAge);
            }
            else if (maxAge != -1 & minAge == -1)
            {
                citizens = citizens.Where(citizen => citizen.Age < maxAge);
            }
            else if (maxAge != -1 & minAge != -1)
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
        public Citizen[] GetCitizen(int id)
        {
            var citizen = _context.Citizens.Where(citizen => citizen.Id == id).Select(citizen => new Citizen{
                Name = citizen.Name,
                Sex = citizen.Sex,
                Age = citizen.Age
            }).ToArray();

            return citizen;
        }
    }
}
