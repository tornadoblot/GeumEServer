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
    public class WalkController : Controller
    {
        ApplicationDbContext _context;

        public WalkController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public string CreateWalk(Walk walk, [FromQuery]string placename, [FromQuery]string doginfo)
        {
            User user = _context.Users
                .Where(item => item.Email == walk.Email)
                .FirstOrDefault();

            if (user == null)
                return "User does not exist";

            _context.Walks.Add(walk);
            _context.SaveChanges();

            int workId = _context.Walks
                .Where(item =>
                    item.Email == walk.Email &&
                    item.Start == walk.Start)
                .FirstOrDefault().Id;

            string[] placelist;
            if (placename != null)
                placelist = placename.Split(',');
            else
                placelist = new string[0];

            for (int i = 0; i < placelist.Length; i++)
            {
                Place place = _context.Places
                    .Where(item => item.Name == placelist[i])
                    .FirstOrDefault();

                if (place == null)
                    return "Place does not exist";

                WalkPlace walkPlace = new WalkPlace()
                {
                    WalkId = workId,
                    PlaceId = place.Id
                };

                _context.WalkPlaces.Add(walkPlace);
            }


            string[] doglist;
            if (doginfo != null)
                doglist = doginfo.Split(',');
            else
                doglist = new string[0];

            for (int i = 0; i < doglist.Length; i += 2)
            {
                Dog dog = _context.Dogs
                    .Where(item => 
                        item.Name == doglist[i] &&
                        item.Email == doglist[i + 1])
                    .FirstOrDefault();

                if (dog == null)
                    return "Dog does not exist";

                WalkDog walkDog = new WalkDog()
                {
                    WalkId = workId,
                    DogId = dog.Id
                };

                _context.WalkDogs.Add(walkDog);
            }


            _context.SaveChanges();
            return "success";
        }

        [HttpGet]
        public List<Walk> GetWalks()
        {
            List<Walk> results = _context.Walks
                .OrderByDescending(item => item.Email)
                .ToList();

            return results;
        }

        [HttpGet("{email}")]
        public List<Walk> GetWalk(string email)
        {

            List<Walk> result = _context.Walks
                .Where(item => item.Email == email)
                .ToList();

            return result;
        }

        [HttpGet("{workId}/place")]
        public List<Place> GetWalkPlace(int workId)
        {
            List<Place> res = new List<Place>();

            List<WalkPlace> workPlace = _context.WalkPlaces
                .Where(item => item.WalkId == workId)
                .ToList();

            if (workPlace == null)
            {
                return null;
            }

            foreach (var i in workPlace)
            {
                Place place = _context.Places
                    .Where(item => item.Id == i.PlaceId)
                    .FirstOrDefault();

                res.Add(place);
            }

            return res;
        }

        [HttpGet("{workId}/dog")]
        public List<Dog> GetWalkDog(int workId)
        {
            List<Dog> res = new List<Dog>();

            List<WalkDog> workDog = _context.WalkDogs
                .Where(item => item.WalkId == workId)
                .ToList();

            if (workDog == null)
            {
                return null;
            }

            foreach (var i in workDog)
            {
                Dog dog = _context.Dogs
                    .Where(item => item.Id == i.DogId)
                    .FirstOrDefault();

                res.Add(dog);
            }

            return res;
        }

        [HttpDelete("{email}")]
        public bool DeleteWalk(string email, [FromBody]DateTime start)
        {
            var findWalk = _context.Walks
                .Where(x => 
                    x.Email == email &&
                    x.Start == start )
                .FirstOrDefault();

            if (findWalk == null)
                return false;

            _context.Walks.Remove(findWalk);
            _context.SaveChanges();

            return true;
        }
    }
}
