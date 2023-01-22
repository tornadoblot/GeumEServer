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
        public string CreateWalk(string[] info)
        {
            User user = _context.Users
                .Where(item => item.Email == info[0])
                .FirstOrDefault(); 

            if (user == null) 
                return "User does not exist";

            Walk walk = new Walk
            {
                User = user,
                Email = user.Email,
                Start = Convert.ToDateTime(info[1]),
                End = Convert.ToDateTime(info[2]),
                Distance = Convert.ToInt32(info[3]),
            };

            if (info.Length > 4)
            {
                int flag = Array.IndexOf(info, "-");

                if (flag < 0)
                    return "Can not find -";

                for (int i = flag - 1; i > 3; i--)
                {
                    Place place = _context.Places
                        .Where(item => item.Name == info[i])
                        .FirstOrDefault();

                    if (place == null)
                        return info[i] + " Place does not exist";

                    place.walk += w
                }

                for (int i = flag + 1; i < info.Length; i++)
                {
                    Dog dog = _context.Dogs
                        .Where(item => item.Name == info[i])
                        .FirstOrDefault();

                    if (dog == null)
                        return info[i] + " Dog does not exist";
                }
            }

            _context.Walks.Add(walk);
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
