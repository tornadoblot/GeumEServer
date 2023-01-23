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

        [HttpPost("{email}")]
        public string ImageUpload(IFormFile img, string email)
        {
            if (img.Length == null)
                return "Image File is null";

            User findUser = FindUser(email);
            if (findUser == null)
                return "Can not find User";

            string path = "/home/ec2-user/GeumEServer/GeumEServer/Upload";
            //string path = "C:\\Users\\Newcl\\Desktop\\GeumEServer\\GeumEServer\\Upload";

            if (findUser.HasImage)
            {
                var tmpFile = Directory.GetFiles(path, email + "*");
                if (tmpFile.Length > 1)
                    return "Can not find Existed Image File";

                System.IO.File.Delete(tmpFile[0]);
            }

            var filename = email + img.FileName[img.FileName.IndexOf(".")..];
            var filePath = Path.Combine(path, filename);
            using (var stream = System.IO.File.Create(filePath))
            {
                img.CopyTo(stream);
            }

            findUser.HasImage = true;            
            _context.SaveChanges();

            return img.Length.ToString();
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
            User result = FindUser(email);
            return result;
        }

        [HttpGet("{email}/image")]
        public string GetUserImage(string email)
        {
            User findUser = FindUser(email);
            if (findUser == null)
                return "Cannot find User";

            string path = "/home/ec2-user/GeumEServer/GeumEServer/Upload";
            //string path = "C:\\Users\\Newcl\\Desktop\\GeumEServer\\GeumEServer\\Upload";

            string filePath;
            if (findUser.HasImage)
            {
                filePath = Directory.GetFiles(path, email + "*")[0];
                filePath = filePath[7..];
            }                
            else
                filePath = "Upload/-.png";

            return "http://13.125.4.157:3200/images/" + filePath;
        }

        [HttpGet("{email}/imageDelete")]
        public string DeleteUserImage(string email)
        {
            User findUser = FindUser(email);
            if (findUser == null)
                return "Cannot find User";

            string path = "/home/ec2-user/GeumEServer/GeumEServer/Upload";
            //string path = "C:\\Users\\Newcl\\Desktop\\GeumEServer\\GeumEServer\\Upload";
            var delFile = Directory.GetFiles(path, email + "*");

            if (delFile.Length > 1)
                return "Can not find Existed Image File";

            System.IO.File.Delete(delFile[0]);
            findUser.HasImage = false;

            _context.SaveChanges();

            return "Delete success";
        }

        // Update
        [HttpPut]
        public bool UpdateUser([FromBody] User user)
        {
            User findUser = FindUser(user.Email, user.Password);
            if (findUser == null)
                return false;

            findUser.Adress = user.Adress;
            findUser.Comment = user.Comment;

            _context.SaveChanges();

            return true;
        }

        // Update Password
        [HttpPut("password")]
        public bool UpdateUserPassword([FromBody]string[] info)
        {
            User findUser = FindUser(info[0]);
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
            var findUser = FindUser(email);
            if (findUser == null)
                return false;

            _context.Users.Remove(findUser);
            _context.SaveChanges();

            return true;
        }

        public User FindUser(string email)
        {
            User findUser = _context.Users
                .Where(x => x.Email == email)
                .FirstOrDefault();

            return findUser;
        }

        public User FindUser(string email, string password)
        {
            User findUser = _context.Users
                .Where(x => 
                    x.Email == email &&
                    x.Password == password)
                .FirstOrDefault();

            return findUser;
        }
    }
}

