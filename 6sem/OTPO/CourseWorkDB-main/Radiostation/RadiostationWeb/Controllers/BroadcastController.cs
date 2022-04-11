using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    public class BroadcastController : Controller
    {
        private readonly RadiostationWebDbContext _dbContext;
        private readonly ApplicationDbContext _applicationDbContext;
        public BroadcastController(RadiostationWebDbContext context, ApplicationDbContext applicationDbContext)
        {
            _dbContext = context;
            _applicationDbContext = applicationDbContext;
        }

        public ActionResult Broadcasts(DateTime? start, DateTime? end, SortState sortOrder = SortState.DateAsc, int page = 1)
        {
            var pageSize = 10;
            var broadcasts = FilterBroadcasts(start, end).ToList();
            var pageBroadcasts = broadcasts.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(broadcasts.Count(), page, pageSize);
            var viewBroadcasts = from b in pageBroadcasts
                                 join e in _dbContext.Employees.ToList() on b.EmployeeId equals e.Id
                                 join a in _applicationDbContext.Users.ToList() on e.AspNetUserId equals a.Id
                                 join r in _dbContext.Records on b.RecordId equals r.Id
                                 select new BroadcastViewModel
                                 {
                                     Id = b.Id,
                                     EmployeeName = a.Name,
                                     EmployeeSurname = a.Surname,
                                     DateAndTime = b.DateAndTime,
                                     RecordName = r.СompositionName,

                                 };
            viewBroadcasts = sortOrder switch
            {
                SortState.NameDsc => viewBroadcasts.OrderByDescending(s => s.EmployeeName),
                SortState.NameAsc => viewBroadcasts.OrderBy(s => s.EmployeeName),
                SortState.SurnameAsc => viewBroadcasts.OrderBy(s => s.EmployeeSurname),
                SortState.SurnameDsc => viewBroadcasts.OrderByDescending(s => s.EmployeeSurname),
                SortState.RecorNameAsc => viewBroadcasts.OrderBy(s => s.RecordName),
                SortState.RecordNameDsc => viewBroadcasts.OrderByDescending(s => s.RecordName),
                SortState.DateDsc => viewBroadcasts.OrderByDescending(s => s.DateAndTime),
                _ => viewBroadcasts.OrderByDescending(s => s.DateAndTime),
            };
            var pageItemsModel = new BroadcastsItemModel 
            {
                Items = viewBroadcasts,
                PageModel = pageViewModel,
                SortViewModel = new SortViewModel(sortOrder)
            };
            return View(pageItemsModel);
        }

        public IActionResult ResetFilter()
        {
            HttpContext.Response.Cookies.Delete("broadcastsFrom");
            HttpContext.Response.Cookies.Delete("broadcastsTo");
            return RedirectToAction(nameof(Broadcasts));
        }

        public IActionResult ResetManageFilter()
        {

            HttpContext.Response.Cookies.Delete("broadcastsFrom");
            HttpContext.Response.Cookies.Delete("broadcastsTo");
            return RedirectToAction(nameof(ManageBroadcasts));
        }
        private IQueryable<Broadcast> FilterBroadcasts(DateTime? start, DateTime? end)
        {

            IQueryable<Broadcast> broadcasts = _dbContext.Broadcasts;
            var cookiesStringStart = HttpContext.Request.Cookies["broadcastsFrom"];
            var cookiesStringEnd = HttpContext.Request.Cookies["broadcastsTo"];
            DateTime? cookiesStart = null;
            DateTime? cookiesEnd = null;
            if (end is null && start is null && !string.IsNullOrEmpty(cookiesStringStart) && !string.IsNullOrEmpty(cookiesStringEnd))
            {
                cookiesStart = DateTime.Parse(cookiesStringStart);
                cookiesEnd = DateTime.Parse(cookiesStringEnd);
            }
            start ??= cookiesStart;
            end ??= cookiesEnd;
            if (start != null && end != null)
            {
                broadcasts = broadcasts.Where(e => e.DateAndTime > start && e.DateAndTime < end);
                HttpContext.Response.Cookies.Append("broadcastsFrom", start.ToString());
                HttpContext.Response.Cookies.Append("broadcastsTo", end.ToString());
            }
            return broadcasts;
        }


        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult ManageBroadcasts(DateTime? start, DateTime? end, SortState sortOrder = SortState.DateAsc, int page = 1)
        {
            var pageSize = 10;
            var broadcasts = FilterBroadcasts(start, end).ToList();
            var pageBroadcasts = broadcasts.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(broadcasts.Count(), page, pageSize);
            var viewBroadcasts = from b in pageBroadcasts
                                 join e in _dbContext.Employees.ToList() on b.EmployeeId equals e.Id
                                 join a in _applicationDbContext.Users.ToList() on e.AspNetUserId equals a.Id
                                 join r in _dbContext.Records on b.RecordId equals r.Id
                                 select new BroadcastViewModel
                                 {
                                     Id = b.Id,
                                     EmployeeName = a.Name,
                                     EmployeeSurname = a.Surname,
                                     DateAndTime = b.DateAndTime,
                                     RecordName = r.СompositionName,

                                 };
            viewBroadcasts = sortOrder switch
            {
                SortState.NameDsc => viewBroadcasts.OrderByDescending(s => s.EmployeeName),
                SortState.NameAsc => viewBroadcasts.OrderBy(s => s.EmployeeName),
                SortState.SurnameAsc => viewBroadcasts.OrderBy(s => s.EmployeeSurname),
                SortState.SurnameDsc => viewBroadcasts.OrderByDescending(s => s.EmployeeSurname),
                SortState.RecorNameAsc => viewBroadcasts.OrderBy(s => s.RecordName),
                SortState.RecordNameDsc => viewBroadcasts.OrderByDescending(s => s.RecordName),
                SortState.DateDsc => viewBroadcasts.OrderByDescending(s => s.DateAndTime),
                _ => viewBroadcasts.OrderByDescending(s => s.DateAndTime),
            };
            var pageItemsModel = new BroadcastsItemModel
            {
                Items = viewBroadcasts,
                PageModel = pageViewModel,
                SortViewModel = new SortViewModel(sortOrder)
            };
            return View(pageItemsModel);
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Delete(int id)
        {
            var broadcast = _dbContext.Broadcasts.Find(id);
            if (broadcast != null)
            {
                try
                {
                    _dbContext.Broadcasts.Remove(broadcast);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home",
                    new { message = "Broadcast contain related data and cannot be deleted" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Broadcast not found" });
            }

            return RedirectToAction(nameof(ManageBroadcasts));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Create()
        {
            var emoployees = _dbContext.Employees.ToList()
                .Join(_applicationDbContext.Users.ToList(),
                e => e.AspNetUserId, t => t.Id,
                (e, t) => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = t.Name + " " + t.Surname,
                }).ToList();
            var records = _dbContext.Records
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.СompositionName }).ToList();


            return View(new CreateBroadcastViewModel { EmployeeList = emoployees, RecordList = records });
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        [HttpPost]
        public ActionResult Create(CreateBroadcastViewModel broadcast)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Broadcasts.Add((new Broadcast
                {
                    EmployeeId = broadcast.EmployeeId,
                    RecordId = broadcast.RecordId,
                    DateAndTime = broadcast.DateAndTime }));
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(ManageBroadcasts));
            }
            var emoployees = _dbContext.Employees.ToList()
                .Join(_applicationDbContext.Users.ToList(),
                e => e.AspNetUserId, t => t.Id,
                (e, t) => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = t.Name + " " + t.Surname,
                }).ToList();
            var records = _dbContext.Records
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.СompositionName }).ToList();

            broadcast.RecordList = records;
            broadcast.EmployeeList = emoployees;

            return View(broadcast);
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Edit(int id)
        {
            var broadcast = _dbContext.Broadcasts.Find(id);
            var emoployees = _dbContext.Employees.ToList()
                .Join(_applicationDbContext.Users.ToList(),
                e => e.AspNetUserId, t => t.Id,
                (e, t) => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = t.Name + " " + t.Surname,
                    Selected = broadcast.EmployeeId == e.Id,

                })
                .ToList();
            var records = _dbContext.Records
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.СompositionName,
                    Selected = broadcast.RecordId == c.Id 
                })
                .ToList();
            if (broadcast != null)
            {
                return View(new EditBroadcastViewModel
                {
                    Id=id,EmployeeList = emoployees,
                    RecordList = records,
                    DateAndTime = broadcast.DateAndTime,
                    EmployeeId=broadcast.EmployeeId,
                    RecordId=broadcast.RecordId
                });
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "broadcast not found" });
            }
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        [HttpPost]
        public ActionResult Edit(EditBroadcastViewModel broadcast)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Broadcasts.Update(new Broadcast 
                {
                    Id = broadcast.Id,
                    EmployeeId = broadcast.EmployeeId,
                    RecordId = broadcast.RecordId,
                    DateAndTime = broadcast.DateAndTime
                });
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return RedirectToAction(nameof(ManageBroadcasts));
                }
            }
            var emoployees = _dbContext.Employees.ToList()
                .Join(_applicationDbContext.Users.ToList(),
                e => e.AspNetUserId, t => t.Id,
                (e, t) => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = t.Name + " " + t.Surname,
                }).ToList();
            var records = _dbContext.Records
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.СompositionName }).ToList();

            broadcast.RecordList = records;
            broadcast.EmployeeList = emoployees;
            return View(broadcast);
        }
    }
}
