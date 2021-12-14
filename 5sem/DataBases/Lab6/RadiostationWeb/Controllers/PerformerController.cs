using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public ActionResult Performers(string nameFilter,string surnameFilter, int page = 1)
        {
            var pageSize = 20;
            var performers = FilterPerformers(nameFilter, surnameFilter);
            var pagePerformers = performers.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(performers.Count(), page, pageSize);
            var viewPerformers = pagePerformers.ToList().Join(_dbContext.Groups.ToList(),
            e => e.Id, t => t.Id,
            (e, t) => new PerformerViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                GroupName = t.Description
            });
            var pageItemsModel = new PageItemsModel<PerformerViewModel> { Items = viewPerformers, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        public IActionResult ResetFilter()
        {

            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("surnameFilter");
            return RedirectToAction(nameof(Performers));
        }

        public IActionResult ResetManageFilter()
        {
            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("surnameFilter");
            return RedirectToAction(nameof(ManagePerformers));
        }
        private IQueryable<Performer> FilterPerformers(string nameFilter, string surnameFilter)
        {
            IQueryable<Performer> performers = _dbContext.Performers;
            nameFilter = nameFilter ?? HttpContext.Request.Cookies["nameFilter"];
            if (!string.IsNullOrEmpty(nameFilter))
            {
                performers = performers.Where(e => e.Name.Contains(nameFilter));
                HttpContext.Response.Cookies.Append("nameFilter", nameFilter);
            }

            surnameFilter = surnameFilter ?? HttpContext.Request.Cookies["surnameFilter"];
            if (!string.IsNullOrEmpty(surnameFilter))
            {
                performers = performers.Where(e => e.Surname.Contains(surnameFilter));
                HttpContext.Response.Cookies.Append("surnameFilter", surnameFilter);
            }

            if (nameFilter == " " && surnameFilter == " ")
            {
                performers = _dbContext.Performers;
            }
            return performers;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManagePerformers(string nameFilter, string surnameFilter, int page = 1)
        {
            var performers = FilterPerformers(nameFilter, surnameFilter);
            var pageSize = 20;
            var pagePerformers = performers.ToList().OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(performers.Count(), page, pageSize);
            var viewPerformers = pagePerformers.ToList().Join(_dbContext.Groups.ToList(),
            e => e.Id, t => t.Id,
            (e, t) => new PerformerViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                GroupName = t.Description
            });
            var pageItemsModel = new PageItemsModel<PerformerViewModel> { Items = viewPerformers, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        [Authorize(Roles = "Admin")]
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

            return RedirectToAction("ManagePerformers");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new Performer());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Performer performer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Performers.Add(performer);
                _dbContext.SaveChanges();
                return RedirectToAction("ManagePerformers");
            }

            return View(performer);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var performer = _dbContext.Performers.Find(id);
            if (performer != null)
            {
                return View(performer);
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Performer not found" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Performer performer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Performers.Update(performer);
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return RedirectToAction(nameof(ManagePerformers));
                }
            }

            return View(performer);
        }
    }
}
