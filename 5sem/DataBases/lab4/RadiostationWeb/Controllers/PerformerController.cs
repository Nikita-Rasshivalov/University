using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    public class PerformerController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public PerformerController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Performers()
        {
            var performers = _dbContext.Performers.ToList();
            var viewPerformers = performers.Join(_dbContext.Groups.ToList(),
                e => e.GroupId, t => t.Id,
                (e, t) => new PerformerViewModel
                {
                    Id=e.Id,
                    Name = e.Name,
                    Surname=e.Surname,
                    GroupName =t.Description
                });
            return View(viewPerformers);

        }
  
    }
}
