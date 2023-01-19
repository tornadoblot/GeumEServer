using GeumEServer.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeumEServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DogController : Controller
    {
        ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Dog CreateDog([FromBody] string[] info)
        {
            User owner = _context.Users
                .Where(item => item.Email == info[0])
                .FirstOrDefault();

            if (owner == null)
                return null;

            Dog dog = new Dog
            {
                Email = owner.Email,
                Name = info[1],
                Species = info[2],
                Birth = Convert.ToDateTime(info[3])
            };

            Dog dupchk = _context.Dogs
                .Where( item =>
                    item.Name == dog.Name &&
                    item.Birth == dog.Birth &&
                    item.Species == dog.Species)
                .FirstOrDefault();

            if (dupchk != null)
                return null;

            _context.Dogs.Add(dog);
            _context.SaveChanges();

            return dog;
        }

        [HttpGet]
        public List<Dog> GetDogs()
        {
            List<Dog> results = _context.Dogs
                .OrderByDescending(item => item.Id)
                .ToList();

            return results;
        }

        [HttpGet("{email}")]
        public List<Dog> GetDog(string email)
        {
            List<Dog> results = _context.Dogs
                .Where(item => item.Email == email)
                .ToList();

            return results;
        }

        [HttpPut]
        public Dog UpdateDog([FromBody] Dog[] dog)
        {
            Dog findDog = _context.Dogs
                .Where(x =>
                    x.Email == dog[0].Email &&
                    x.Name == dog[0].Name &&
                    x.Species == dog[0].Species &&
                    x.Birth == dog[0].Birth)
                .FirstOrDefault();

            if (findDog == null)
                return null;

            findDog.Name = dog[1].Name;
            findDog.Species = dog[1].Species;
            findDog.Birth = dog[1].Birth;

            _context.SaveChanges();

            return findDog;
        }

        [HttpDelete]
        public bool DeleteDog([FromBody] Dog dog)
        {
            var findDog = _context.Dogs
                .Where(x =>
                    x.Email == dog.Email &&
                    x.Name == dog.Name &&
                    x.Species == dog.Species &&
                    x.Birth == dog.Birth)
                .FirstOrDefault();

            if (findDog == null)
                return false;

            _context.Dogs.Remove(findDog);
            _context.SaveChanges();

            return true;
        }
    }
}
