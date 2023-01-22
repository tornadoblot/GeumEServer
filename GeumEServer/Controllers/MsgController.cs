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
    public class MsgController : Controller
    {
        ApplicationDbContext _context;

        public MsgController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public string SendMsg(Msg msg)
        {
            User user = _context.Users
                .Where(item => item.Email == msg.recieveEmail)
                .FirstOrDefault();

            if(user == null)
                return "User does not exist";

            _context.Msgs.Add(msg);
            _context.SaveChanges();

            return msg.recieveEmail;
        }

        [HttpGet("{email}")]
        public List<Msg> GetRecvMsgs(string email)
        {
            var results = _context.Msgs
                .Where(item => item.recieveEmail == email)
                .ToList();

            return results;
        }

        [HttpGet("{email}/send")]
        public List<Msg> GetSendMsgs(string email)
        {
            var results = _context.Msgs
                .Where(item => item.recieveEmail == email)
                .ToList();

            return results;
        }
    }
}
