using GeumEServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace GeumEServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        // Read All User
        [HttpGet]
        public List<User> GetUsers()
        {
            List<User> results = _context.Users
                .OrderByDescending(item => item.UserId)
                .ToList();

            return results;
        }

        // Read
        [HttpGet("{email}")]
        public User GetUser(string email)
        {
            User result = _context.Users
                .Where(item => item.Email == email)
                .FirstOrDefault();

            return result;
        }

        // Update
        [HttpPut]
        public bool UpdateUser([FromBody] User user)
        {
            User findUser = _context.Users
                .Where(item => item.Email == user.Email)
                .FirstOrDefault();

            if (findUser == null)
                return false;

            findUser.Adress = user.Adress;
            findUser.Comment = user.Comment;

            _context.SaveChanges();

            return true;
        }



        [HttpPost("{email}")]
        public string ImageUpload(IFormFile img, string email)
        {
            if (img == null)
                return "null";

            if (img.Length > 0)
            {

                //User findUser = _context.Users
                //    .Where(x => x.Email == email)
                //    .FirstOrDefault();

                var path = Path.Combine($"Upload");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path); // 웹 서비스 내 업로드폴더가 없을 경우 자동생성을 위한 처리
                }
                var filename = email + img.FileName[img.FileName.IndexOf(".")..]; // 동일한 파일명이 있으면 덮어쓰거나, 오류가 날 수 있으므로 파일명을 바꾼다.
                path = Path.Combine(path, filename);
                using (var stream = System.IO.File.Create(path))
                {
                    img.CopyTo(stream);
                }

            }

            return img.Length.ToString();

        }

        // Update Password
        [HttpPut("password")]
        public bool UpdateUserPassword([FromBody]string[] info)
        {
            User findUser = _context.Users
                .Where(item => item.Email == info[0])
                .FirstOrDefault();

            if (findUser == null)
                return false;

            findUser.Password = info[1];

            _context.SaveChanges();

            return true;
        }

        // Delete
        [HttpDelete("{email}")]
        public bool DeleteUser(string email)
        {
            var findUser = _context.Users
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (findUser == null)
                return false;

            _context.Users.Remove(findUser);
            _context.SaveChanges();

            return true;
        }
    }
}
