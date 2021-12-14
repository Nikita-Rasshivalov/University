using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
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


        [Authorize]
        public ActionResult Employees(string nameFilter, string surnameFilter, int page = 1)
        {
            var pageSize = 20;
            var employees = FilterEmployees(nameFilter, surnameFilter);
            var pageEmployees = employees.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(employees.Count(), page, pageSize);
            var viewEmployees = pageEmployees.ToList();

            var pageItemsModel = new PageItemsModel<Employee> { Items = viewEmployees, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        public IActionResult ResetFilter()
        {

            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("surnameFilter");
            return RedirectToAction(nameof(Employees));

        }
        private IQueryable<Employee> FilterEmployees(string nameFilter, string surnameFilter)
        {
            IQueryable<Employee> employees = _dbContext.Employees;
            nameFilter = nameFilter ?? HttpContext.Request.Cookies["nameFilter"];
            if (!string.IsNullOrEmpty(nameFilter))
            {
                employees = employees.Where(e => e.Name.Contains(nameFilter));
                HttpContext.Response.Cookies.Append("nameFilter", nameFilter);
            }

            surnameFilter = surnameFilter ?? HttpContext.Request.Cookies["surnameFilter"];
            if (!string.IsNullOrEmpty(surnameFilter))
            {
                employees = employees.Where(e => e.Surname.Contains(surnameFilter));
                HttpContext.Response.Cookies.Append("surnameFilter", surnameFilter);
            }

            if (nameFilter == " " && surnameFilter == " ")
            {
                employees = _dbContext.Employees;
            }
            return employees;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageEmployees(int page = 1)
        {
            IQueryable<Employee> employees = _dbContext.Employees;
            var pageSize = 20;
            var pageEmployees = employees.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(employees.Count(), page, pageSize);
            var viewEmployees = pageEmployees.ToList();
            var pageItemsModel = new PageItemsModel<Employee> { Items = viewEmployees, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null)
            {
                try
                {
                    _dbContext.Employees.Remove(employee);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home",
                    new { message = "Employee contain related data and cannot be deleted" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Employee not found" });
            }

            return RedirectToAction("ManageEmployees");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new Employee());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("ManageEmployees");
            }

            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null)
            {
                return View(employee);
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Employee not found" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Employee employee, string returnUrl = "Employees")
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Update(employee);
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return Redirect(returnUrl);
                }
            }

            return View(employee);
        }
    }
}

