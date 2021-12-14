using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
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

        [Authorize]
        public ActionResult Groups(string groupFilter, int page = 1)
        {
            var pageSize = 20;
            var groups = FilterGroups(groupFilter);
            var pageGroups = groups.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(groups.Count(), page, pageSize);
            var viewGroups = pageGroups.ToList();

            var pageItemsModel = new PageItemsModel<Group> { Items = viewGroups, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        public IActionResult ResetFilter()
        {

            HttpContext.Response.Cookies.Delete("groupFilter");
            return RedirectToAction(nameof(Groups));

        }
        private IQueryable<Group> FilterGroups(string groupFilter)
        {
            IQueryable<Group> groups = _dbContext.Groups;
            groupFilter = groupFilter ?? HttpContext.Request.Cookies["groupFilter"];
            if (!string.IsNullOrEmpty(groupFilter))
            {
                groups = groups.Where(e => e.Description.Contains(groupFilter));
                HttpContext.Response.Cookies.Append("groupFilter", groupFilter);
            }

         
            if (groupFilter == " ")
            {
                groups = _dbContext.Groups;
            }
            return groups;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageGroups(int page = 1)
        {
            IQueryable<Group> groups = _dbContext.Groups;
            var pageSize = 20;
            var pageGroups = groups.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(groups.Count(), page, pageSize);
            var viewGroups = pageGroups.ToList();
            var pageItemsModel = new PageItemsModel<Group> { Items = viewGroups, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var group = _dbContext.Groups.Find(id);
            if (group != null)
            {
                try
                {
                    _dbContext.Groups.Remove(group);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home",
                    new { message = "Group contain related data and cannot be deleted" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Group not found" });
            }

            return RedirectToAction("ManageGroups");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new Group());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Groups.Add(group);
                _dbContext.SaveChanges();
                return RedirectToAction("ManageGroups");
            }

            return View(group);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var group = _dbContext.Groups.Find(id);
            if (group != null)
            {
                return View(group);
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Group not found" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Groups.Update(group);
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return RedirectToAction(nameof(ManageGroups));
                }
            }

            return View(group);
        }

    }
}
