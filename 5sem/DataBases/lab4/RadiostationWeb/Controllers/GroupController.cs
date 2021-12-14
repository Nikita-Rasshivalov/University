using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using System.Linq;


namespace RadiostationWeb.Controllers
{
    public class GroupController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public GroupController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Groups()
        {
            return View(_dbContext.Groups.ToList());
        }
    }
}
