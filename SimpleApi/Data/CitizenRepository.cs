using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

namespace SimpleApi.Data
{
    public class CitizenRepository : ICitizenRepository
    {
        private CitizenContext _context;
        private bool disposed;
 
        public CitizenRepository(CitizenContext context)
        {
            _context = context;
        }
 
        public IEnumerable<object> GetCitizens(int? page,int? numberOfCitizens, int? minAge, int? maxAge, string sex = null)
        {
            List<Citizen> citizens;

            citizens = sex != null ? _context.Citizens.Where(citizen => citizen.Sex == sex).ToList() : _context.Citizens.ToList();

            if (minAge != null & maxAge == null)
            {
                citizens = citizens.Where(citizen => citizen.Age > minAge).ToList();
            }
            else if (maxAge != null & minAge == null)
            {
                citizens = citizens.Where(citizen => citizen.Age < maxAge).ToList();
            }
            else if (maxAge != null & minAge != null)
            {
                citizens = citizens.Where(citizen => citizen.Age > minAge & citizen.Age < maxAge).ToList();
            }
            
            int pageNum = page ?? 1;
            int pageSize = numberOfCitizens ?? 10;
            
            return citizens.Select(citizen => new {
                id = citizen.Id,
                name = citizen.Name,
                sex = citizen.Sex
            }).Skip(pageNum * pageSize).Take(pageSize).ToList();
        }
 
        public object GetCitizen(int id)
        {
            var citizen = _context.Citizens.Where(citizen => citizen.Id == id).Select(citizen => new {
                name = citizen.Name,
                sex = citizen.Sex,
                age = citizen.Age
            }).ToArray();

            return citizen;
        }
 
        public void Create(Citizen citizen)
        {
            _context.Citizens.Add(citizen);
        }
 
        public void Update(Citizen citizen)
        {
            _context.Entry(citizen).State = EntityState.Modified;
        }
 
        public void Delete(int id)
        {
            Citizen citizen = _context.Citizens.Find(id);
            
            if(citizen!=null)
                _context.Citizens.Remove(citizen);
        }
 
        public void Save()
        {
            _context.SaveChanges();
        }
 
        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}