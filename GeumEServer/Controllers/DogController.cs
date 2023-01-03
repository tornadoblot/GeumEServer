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
        public bool UpdateDog([FromBody] Dog[] dog)
        {
            Dog findDog = _context.Dogs
                .Where(item => item.Email == dog[0].Email && item.Name == dog[0].Name)
                .FirstOrDefault();

            if (findDog == null)
                return false;

            findDog.Name = dog[1].Name;

            _context.SaveChanges();

            return true;
        }

        [HttpDelete("{email}")]
        public bool DeleteDog(Dog dog)
        {
            var findDog = _context.Dogs
                .Where(x => x.Email == dog.Email && x.Name == dog.Name)
                .FirstOrDefault();

            if (findDog == null)
                return false;

            _context.Dogs.Remove(findDog);
            _context.SaveChanges();

            return true;
        }
    }
}
