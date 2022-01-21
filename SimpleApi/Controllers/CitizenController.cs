using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Data;
using SimpleApi.Services;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenService _service;

        public CitizenController(CitizenContext context)
        {
           _service = new CitizenService(new CitizenRepository(context));
        }
        
        [HttpGet]
        public IEnumerable<object> GetCitizens(int? page,int? numberOfCitizens, int? minAge, int? maxAge, string sex = null)
        {
            return _service.GetCitizens(page, numberOfCitizens, minAge, maxAge, sex);
        }

        [HttpGet("{id}")]
        public object GetCitizen(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            return _service.GetCitizen(id);
        }
    }
}
