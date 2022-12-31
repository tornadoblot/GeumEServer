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

    public class DogController : Controller
    {
        ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
