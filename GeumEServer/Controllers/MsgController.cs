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

            if (user == null)
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
                .Where(item => item.sendEmail == email)
                .ToList();

            return results;
        }

        [HttpDelete("{sendEmail}/{recvEmail}")]
        public int DeleteMsgs(string sendEmail, string recvEmail)
        {
            int cnt = 0;

            var sendRes = _context.Msgs
                .Where(i => i.sendEmail == sendEmail && i.recieveEmail == recvEmail)
                .ToList();

            var recvRes = _context.Msgs
                .Where(i => i.sendEmail == recvEmail && i.recieveEmail == sendEmail)
                .ToList();

            foreach (var i in sendRes)
            {
                i.sendDel = true;

                if(i.sendDel == i.recDel)
                {
                    _context.Msgs.Remove(i);
                    cnt++;
                }
            }

            foreach (var i in recvRes)
            {
                i.recDel = true;

                if (i.sendDel == i.recDel)
                {
                    _context.Msgs.Remove(i);
                    cnt++;
                }
            }

            _context.SaveChanges();
            return cnt;
        }
    }
}
