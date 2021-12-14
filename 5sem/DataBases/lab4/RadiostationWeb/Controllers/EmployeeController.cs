using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public EmployeeController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Employees()
        {
            return View(_dbContext.Employees.ToList());
        }
    }
}

