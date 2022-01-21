using System.Collections.Generic;
using SimpleApi.Models;

namespace SimpleApi.Services
{
    public interface ICitizenService
    {
        IEnumerable<object> GetCitizens(int? page,int? numberOfCitizens, int? minAge, int? maxAge, string sex = null);
        object GetCitizen(int id);
        void Create(Citizen item);
        void Update(Citizen item);
        void Delete(int id);
        void Save();
    }
}