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

            User findUser = _context.Users
                            .Where(x => x.Email == email)
                            .FirstOrDefault();

            Ranking ranking = _context.Rankings
                            .Where(x => x.UserId == findUser.UserId)
                            .FirstOrDefault();

            if (ranking == null)
                return "Can not find Ranking Info";

            List<int> dogList = new List<int>();

            string[] emotion = new string[30];
            int cnt = 0;

            foreach (var i in results)
            {
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

            TimeSpan totalTime = ranking.time - DateTime.MinValue;
            string time = totalTime.TotalHours.ToString();
            int distance = ranking.distance;

            string res = GetUserRanking(findUser.UserId) + "/" + time + "/" + distance.ToString() + "/";
            res += GetMostViewedDog(dogList);

            foreach (var i in emotion)
            {
                res += i + " ";
            }

            return res;        
        }

        private string GetUserRanking(int userId)
        {
            string res = "";

            List<Ranking> rankings = _context.Rankings.ToList();


            rankings = rankings.OrderByDescending(i => i.time).ToList();
            res += (rankings.FindIndex(i => i.UserId == userId) + 1).ToString() + "/";

            rankings = rankings.OrderByDescending(i => i.distance).ToList();
            res += (rankings.FindIndex(i => i.UserId == userId) + 1).ToString();

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

        private void InitRanking()
        {
            // 그동안의 산책 정보를 가져오기
            // 가져와서 해당 유저가 산책한 거리와 산책한 시간을 추가하기
            // 만약 유저 정보가 없다면 새로 생성하기

            List<Walk> walks = _context.Walks
                                .ToList();

            List<Ranking> add = new List<Ranking>();

            foreach (var i in walks)
            {
                User findUser = _context.Users
                            .Where(x => x.Email == i.Email)
                            .FirstOrDefault();

                Ranking user = add.Find(x => x.UserId == findUser.UserId);

                if (user == null)
                {
                    user = new Ranking
                    {
                        UserId = findUser.UserId,
                        distance = 0,
                        time = DateTime.MinValue
                    };
                }

                user.distance += i.Distance;
                user.time += i.End - i.Start;
                add.Add(user);
            }

            _context.Rankings.AddRange(add);
            _context.SaveChanges();

            return;
        }
    }
}
