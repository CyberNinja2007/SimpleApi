using System.Collections.Generic;
using SimpleApi.Data;
using SimpleApi.Models;

namespace SimpleApi.Services
{
    public class CitizenService : ICitizenService
    {
        private CitizenRepository _repository;
        
        public CitizenService(CitizenRepository repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<object> GetCitizens(int? page, int? numberOfCitizens, int? minAge, int? maxAge, string sex = null)
        {
            return _repository.GetCitizens(page, numberOfCitizens, minAge, maxAge, sex);
        }

        public object GetCitizen(int id)
        {
            return _repository.GetCitizen(id);
        }

        public void Create(Citizen item)
        {
            _repository.Create(item);
        }

        public void Update(Citizen item)
        {
            _repository.Update(item);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Save()
        {
            _repository.Save();
        }
    }
}