﻿using GeumEServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost("walking")]
        public string StartWalk(Walking walking)
        {
            _context.Walkings.Add(walking);
            _context.SaveChanges();

            return "Start Walk..";
        }

        [HttpGet("walking")]
        public List<Walking> GetWalking()
        {
            List<Walking> results = _context.Walkings
                .ToList();

            return results;
        }

        [HttpPut("walking")]
        public string UpdateWalking(Walking walking)
        {
            Walking findWalking = _context.Walkings
                .Where(i => i.dogId == walking.dogId)
                .FirstOrDefault();

            if (findWalking == null)
                return "Cannot find Walking";

            findWalking.lat = walking.lat;
            findWalking.log = walking.log;

            _context.SaveChanges();

            return "Update Complete";
        }

        [HttpDelete("walking")]
        public string EndWalk(Walking walking)
        {
            Walking findWalking = _context.Walkings
                .Where(i => i.dogId == walking.dogId)
                .FirstOrDefault();

            if (findWalking == null)
                return "Cannot find Walking";

            _context.Walkings.Remove(findWalking);
            _context.SaveChanges();

            return "End Walk..";
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

            int workId = GetWalkId(walk.Email, walk.Start);

            if (AddWalkPlace(workId, placename))
                return "Place does not exist";

            if (AddDogPlace(workId, doginfo))
                return "Dog does not exist";

            AddRankingInfo(walk);

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

        [HttpGet("path")]
        public string RecommendPath([FromQuery]decimal lat, [FromQuery]decimal log)
        {
            decimal[,] res = new decimal[3, 2];
            decimal[] resDis = new decimal[3];
            for (int i = 0; i < 3; i++)
            {
                resDis[i] = 99999;
            }

            foreach (var i in _context.Places)
            {
                decimal distance = GetDistance(lat, log, i.lat, i.log);

                for (int j = 0; j < 3; j++)
                {
                    if (resDis[j] > distance)
                    {
                        for(int k = 2; k > j; k--)
                        {
                            resDis[k] = resDis[k - 1];
                            res[k, 0] = res[k - 1, 0];
                            res[k, 1] = res[k - 1, 1];
                        }

                        resDis[j] = distance;
                        res[j, 0] = i.lat;
                        res[j, 1] = i.log;
                        break;
                    }
                }
            }

            return   res[1, 1].ToString() + "," + res[1, 0].ToString() + "_"
                   + res[2, 1].ToString() + "," + res[2, 0].ToString();
        }

        [HttpPost("{walkname}")]
        public string VideoUpload(IFormFile vid, string walkname)
        {
            if (vid == null)
                return "File is null";

            string path = Path.Combine($"Upload");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filename = walkname + vid.FileName[vid.FileName.IndexOf(".")..];
            var filePath = Path.Combine(path, filename);
            using (var stream = System.IO.File.Create(filePath))
            {
                vid.CopyTo(stream);
            }

            return vid.Length.ToString();
        }

        public int GetWalkId(string email, DateTime start)
        {
            return _context.Walks
                .Where(item =>
                    item.Email == email &&
                    item.Start == start)
                .FirstOrDefault().Id;
        }

        public bool AddWalkPlace(int workId, string placename)
        {
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
                    return true;

                WalkPlace walkPlace = new WalkPlace()
                {
                    WalkId = workId,
                    PlaceId = place.Id
                };

                _context.WalkPlaces.Add(walkPlace);
            }

            return false;
        }

        public bool AddDogPlace(int workId, string doginfo)
        {
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
                    return true;

                WalkDog walkDog = new WalkDog()
                {
                    WalkId = workId,
                    DogId = dog.Id
                };

                _context.WalkDogs.Add(walkDog);
            }

            return false;
        }

        private decimal GetDistance(decimal lat, decimal log, decimal lat2, decimal log2)
        {
            return Math.Abs(lat - lat2) + Math.Abs(log - log2);
        }

        private void AddRankingInfo(Walk walk)
        {
            User findUser = _context.Users
                            .Where(x => x.Email == walk.Email)
                            .FirstOrDefault();

            Ranking ranking = _context.Rankings
                                .Where(x => x.UserId == findUser.UserId)
                                .FirstOrDefault();

            if(ranking == null)
            {
                ranking = new Ranking
                {
                    UserId = findUser.UserId,
                    distance = 0,
                    time = DateTime.MinValue
                };

                _context.Rankings.Add(ranking);
            }

            ranking.time += walk.End - walk.Start;
            ranking.distance += walk.Distance;

            _context.SaveChanges();
        }
    }
}
