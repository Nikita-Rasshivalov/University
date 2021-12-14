using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Controllers
{
    public class GenreController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public GenreController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Genres()
        {
            return View(_dbContext.Genres.ToList());
        }
    }
}
