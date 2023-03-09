using GeumEServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeumEServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{email}")]
        public string GetReport(string email)
        {
            // 순위 / 거리 / 시간 / 친친 / 감정

            List<Walk> results = _context.Walks
                .Where(item => item.Email == email)
                .OrderByDescending(item => item.Id)
                .ToList();

            DateTime time = DateTime.MinValue;
            int distance = 0;
            List<int> dogList = new List<int>();

            string[] emotion = new string[30];
            int cnt = 0;

            foreach (var i in results)
            {
                time += i.End - i.Start;
                distance += i.Distance;

                List<WalkDog> walkDogs = _context.WalkDogs
                    .Where(item => item.WalkId == i.Id)
                    .ToList();

                foreach(var j in walkDogs)
                {
                    dogList.Add(j.DogId);
                }

                if(cnt < 30 && i.Emotion != null)
                {
                    emotion[cnt++] = i.Emotion + " " + i.Start.Day;
                }
            }            

            string res = time + "/" + distance.ToString() + "/";
            res += GetMostViewedDog(dogList);

            foreach (var i in emotion)
            {
                res += i + " ";
            }

            return res;        
        }

        private string GetMostViewedDog(List<int> dogList)
        {
            dogList.Sort();
            if (dogList.Count > 0)
            {
                int left = 0, leftcnt = 0, now = dogList.First(), nowcnt = 0;
                foreach (var i in dogList)
                {
                    if (now == i)
                    {
                        nowcnt++;

                        if (nowcnt > leftcnt)
                        {
                            left = i;
                            leftcnt = nowcnt;
                        }
                    }
                    else
                    {
                        now = i;
                        nowcnt = 0;
                    }
                }

                Dog dogName = _context.Dogs
                                    .Where(item => item.Id == left)
                                    .FirstOrDefault();

                return dogName.Name + " " + leftcnt.ToString() + "/";
            }
            else
            {
                return "/";
            }            
        }
    }
}
