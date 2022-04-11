using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    public class PerformerController : Controller
    {
        private readonly RadiostationWebDbContext _dbContext;
        public PerformerController(RadiostationWebDbContext context)
        {
            _dbContext = context;
        }
        [Authorize]
        public ActionResult Performers(string nameFilter,int? groupFilter, string surnameFilter, int page = 1)
        {
            var pageSize = 10;
            var performers = FilterPerformers(nameFilter, groupFilter, surnameFilter).ToList();
            var pagePerformers = performers.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(performers.Count(), page, pageSize);
            var groups = _dbContext.Groups.ToList();

            var viewPerformers = from b in pagePerformers
                                 select new PerformerViewModel
                                 {
                                     Id = b.Id,
                                     Name = b.Name,
                                     Surname = b.Surname,
                                     GroupName = groups.FirstOrDefault(g => g.Id == b.GroupId)?.Description,
                                 };

            var groupsList = _dbContext.Groups.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description }).ToList();
            var pageItemsModel = new PerformersItemsModel { Items = viewPerformers, PageModel = pageViewModel, SelectGroups = groupsList };
            return View(pageItemsModel);
        }


        public IActionResult ResetFilter()
        {
            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("groupFilter");
            HttpContext.Response.Cookies.Delete("surnameFilter");
            return RedirectToAction(nameof(Performers));
        }

        public IActionResult ResetManageFilter()
        {
            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("groupFilter");
            HttpContext.Response.Cookies.Delete("surnameFilter");
            return RedirectToAction(nameof(ManagePerformers));
        }

        private IQueryable<Performer> FilterPerformers(string nameFilter, int? groupFilter, string surnameFilter)
        {
            IQueryable<Performer> performers = _dbContext.Performers;
            nameFilter = nameFilter ?? HttpContext.Request.Cookies["nameFilter"];
            if (!string.IsNullOrEmpty(nameFilter))
            {
                performers = performers.Where(e => e.Name.Contains(nameFilter));
                HttpContext.Response.Cookies.Append("nameFilter", nameFilter);
            }
            int cookieGroupFilter;
            int.TryParse(HttpContext.Request.Cookies["groupFilter"], out cookieGroupFilter);
            groupFilter = groupFilter ?? cookieGroupFilter;
            if (groupFilter != 0)
            {
                performers = performers.Where(e => e.GroupId == groupFilter);
                HttpContext.Response.Cookies.Append("groupFilter", groupFilter.ToString());
            }

            surnameFilter = surnameFilter ?? HttpContext.Request.Cookies["surnameFilter"];
            if (!string.IsNullOrEmpty(surnameFilter))
            {
                performers = performers.Where(e => e.Surname.Contains(surnameFilter));
                HttpContext.Response.Cookies.Append("surnameFilter", surnameFilter);
            }

            return performers;
        }


        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult ManagePerformers(string nameFilter,int? groupFilter, string surnameFilter, int page = 1)
        {
            var performers = FilterPerformers(nameFilter,groupFilter, surnameFilter).ToList();
            var pageSize = 10;
            var pagePerformers = performers.ToList().OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(performers.Count(), page, pageSize);
            var groups = _dbContext.Groups.ToList();
            var viewPerformers = from b in pagePerformers
                                 select new PerformerViewModel
                                 {
                                     Id = b.Id,
                                     Name = b.Name,
                                     Surname = b.Surname,
                                     GroupName = groups.FirstOrDefault(g => g.Id == b.GroupId)?.Description,
                                 };
            var groupsList = _dbContext.Groups.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description }).ToList();
            var pageItemsModel = new PerformersItemsModel { Items = viewPerformers, PageModel = pageViewModel, SelectGroups = groupsList };
            return View(pageItemsModel);
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Delete(int id)
        {
            var performer = _dbContext.Performers.Find(id);
            if (performer != null)
            {
                try
                {
                    _dbContext.Performers.Remove(performer);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home",
                    new { message = "Performer contain related data and cannot be deleted" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Performer not found" });
            }

            return RedirectToAction(nameof(ManagePerformers));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Create()
        {
            var groups = _dbContext.Groups.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description }).ToList();
            return View(new CreatePerformerViewModel { GroupsList = groups });
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        [HttpPost]
        public ActionResult Create(CreatePerformerViewModel performer)
        {
            if (ModelState.IsValid)
            {

                if (performer.GroupId == 0)
                {
                    _dbContext.Performers.Add(new Performer
                    {
                        Name = performer.Name,
                        Surname = performer.Surname,
                    });
                }
                else
                {
                    _dbContext.Performers.Add(new Performer
                    {
                        Name = performer.Name,
                        Surname = performer.Surname,
                        GroupId = performer.GroupId
                    });
                }

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(ManagePerformers));
            }
            var groups = _dbContext.Groups.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description }).ToList();
            performer.GroupsList = groups;
            return View(performer);
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Edit(int id)
        {

            var performer = _dbContext.Performers.Find(id);
            var groups = _dbContext.Groups.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Description,
                Selected = performer.GroupId == c.Id,
            }).ToList();
            if (performer != null)
            {
                return View(new EditPerformerViewModel
                {
                    Id = id,
                    GroupsList = groups,
                    Surname = performer.Surname,
                    Name = performer.Name,
                    GroupId = performer.GroupId,
                });
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Performer not found" });
            }
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        [HttpPost]
        public ActionResult Edit(EditPerformerViewModel performer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Performers.Update(new Performer
                {
                    Id = performer.Id,
                    GroupId = performer.GroupId,
                    Name = performer.Name,
                    Surname = performer.Surname
                });
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return RedirectToAction(nameof(ManagePerformers));
                }
            }
            var groups = _dbContext.Groups.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description }).ToList();
            performer.GroupsList = groups;
            return View(performer);
        }
    }
}
