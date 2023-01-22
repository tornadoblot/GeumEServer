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
    public class PlaceController : Controller
    {
        ApplicationDbContext _context;

        public PlaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Place CreatePlace([FromBody] Place place)
        {
            _context.Places.Add(place);
            _context.SaveChanges();

            return place;
        }

        [HttpGet]
        public List<Place> GetPlace()
        {
            List<Place> results = _context.Places
                .OrderByDescending(item => item.Id)
                .ToList();

            return results;
        }

        [HttpGet("{area}")]
        public List<Place> GetPlace(int area)
        {
            List<Place> results = _context.Places
                .Where(item => item.Area == area)
                .ToList();

            return results;
        }

        [HttpGet("{area}/{kind}")]
        public List<Place> GetPlace(int area, string kind)
        {
            List<Place> results = _context.Places
                .Where(item =>
                        item.Area == area &&
                        item.Kind == kind)
                .ToList();

            return results;
        }

        [HttpDelete("{place}")]
        public bool DeletePlace(string place)
        {
            var findPlace = _context.Places
                .Where(x => x.Name == place)
                .FirstOrDefault();

            if (findPlace == null)
                return false;

            _context.Places.Remove(findPlace);
            _context.SaveChanges();

            return true;
        }
    }
}

