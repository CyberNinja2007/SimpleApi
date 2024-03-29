using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SimpleApi.Controllers;
using SimpleApi.Data;
using SimpleApi.Models;

namespace SimpleApi.Tests
{
    [TestFixture]
    public class CitizenControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                if (!context.Citizens.Any())
                {
                    context.Citizens.AddRange(new Citizen[]
                    {
                        new Citizen { Age = 38, Name = "Demian Goal", Sex = "male" },
                        new Citizen { Age = 23, Name = "Naruto Uzumaki", Sex = "male" },
                        new Citizen { Age = 60, Name = "Peter Parker", Sex = "male" },
                        new Citizen { Age = 86, Name = "Jackie Black", Sex = "female" },
                        new Citizen { Age = 14, Name = "Abby Fir", Sex = "female" }
                    });
                    context.SaveChanges();
                }
            }
        }
        
        [Test]
        public void GetCitizens_ShouldReturnAllCitizens()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                var controller = new CitizenController(context);
                var rightData = context.Citizens.Select(citizen => new 
                {
                    id = citizen.Id,
                    name = citizen.Name,
                    sex = citizen.Sex 
                }).Skip(1 * 10).Take(10).ToList();
                
                Assert.AreEqual(rightData.ToString(),controller.GetCitizens(null,null,null,null).ToString());
            }
        }
        
        [Test]
        public void GetCitizens_ShouldReturnAllMaleCitizens()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                var controller = new CitizenController(context);
                var rightData = context.Citizens.Where(cit => cit.Sex == "male").Select(citizen => new 
                {
                    id = citizen.Id,
                    name = citizen.Name,
                    sex = citizen.Sex 
                }).Skip(1 * 10).Take(10).ToList();
                
                Assert.AreEqual(rightData.ToString(),controller.GetCitizens(null,null,null,null,sex:"male").ToString());
            }
        }
        
        [Test]
        public void GetCitizens_ShouldReturnAllCitizensMore20()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                var controller = new CitizenController(context);
                var rightData = context.Citizens.Where(cit => cit.Age > 20).Select(citizen => new 
                {
                    id = citizen.Id,
                    name = citizen.Name,
                    sex = citizen.Sex 
                }).Skip(1 * 10).Take(10).ToList();
                
                Assert.AreEqual(rightData.ToString(),controller.GetCitizens(null,null,null,20).ToString());
            }
        }
        
        [Test]
        public void GetCitizens_ShouldReturnAllCitizensLess50()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                var controller = new CitizenController(context);
                var rightData = context.Citizens.Where(cit => cit.Age < 50).Select(citizen => new 
                {
                    id = citizen.Id,
                    name = citizen.Name,
                    sex = citizen.Sex 
                }).Skip(1 * 10).Take(10).ToList();
                
                Assert.AreEqual(rightData.ToString(),controller.GetCitizens(null,null,0,50).ToString());
            }
        }
        
        [Test]
        public void GetCitizens_ShouldReturnAllFemaleCitizensMore30Less50()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                var controller = new CitizenController(context);
                var rightData = context.Citizens.Where(cit => cit.Sex == "female" & cit.Age > 30 & cit.Age < 50)
                    .Select(citizen => new 
                    { 
                        id = citizen.Id,
                        name = citizen.Name,
                        sex = citizen.Sex 
                    }).Skip(1 * 10).Take(10).ToList();
                
                Assert.AreEqual(rightData.ToString(),controller.GetCitizens(null,null,30,50,"female").ToString());
            }
        }
        
        [Test]
        public void GetCitizen_ShouldReturnFirstCitizen()
        {
            var options = new DbContextOptionsBuilder<CitizenContext>()
                .UseSqlServer(new SqlConnection(
                    "Data Source=localhost;Initial Catalog=Citizens;Trusted_Connection=True;MultipleActiveResultSets=True"))
                .Options;

            using(var context = new CitizenContext(options))
            {
                var controller = new CitizenController(context);

                var rightData = context.Citizens.Where(citizen => citizen.Id == 1).Select(citizen => new
                {
                    name = citizen.Name,
                    sex = citizen.Sex,
                    age = citizen.Age
                }).ToArray();
                
                Assert.AreEqual(rightData.ToString(),controller.GetCitizen(1).ToString());
            }
        }
    }
}