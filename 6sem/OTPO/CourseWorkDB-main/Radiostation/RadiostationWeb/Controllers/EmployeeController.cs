using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly RadiostationWebDbContext _dbContext;
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeController(RadiostationWebDbContext context, ApplicationDbContext applicationDbContext)
        {
            _dbContext = context;
            _applicationDbContext = applicationDbContext;
        }


        [Authorize]
        public ActionResult Employees(int page = 1)
        {
            var pageSize = 10;
            var employees = _dbContext.Employees.ToList();
            var pageEmployees = employees.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(employees.Count(), page, pageSize);
            var positions = _dbContext.Positions.ToList();
            var viewEmployees = from e in employees
                                 join a in _applicationDbContext.Users.ToList() on e.AspNetUserId equals a.Id
                                 select new EmployeeViewModel
                                 {
                                     Id = e.Id,
                                     AspNetUserId = e.AspNetUserId,
                                     Name = a.Name,
                                     Surname = a.Surname,
                                     MiddleName = a.MiddleName,
                                     Username = a.UserName,
                                     Email = a.Email,
                                     Education = e.Education,
                                     PositionName = positions.FirstOrDefault(g => g.Id == e.PositionId)?.Name,
                                     WorkTime = e.WorkTime,
                                 };
            var pageItemsModel = new PageItemsModel<EmployeeViewModel> { Items = viewEmployees, PageModel = pageViewModel };
            return View(pageItemsModel);
        }
    }
}

