using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    public class RecordController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public RecordController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [Authorize]
        public ActionResult Records(string nameFilter, int? performerFilter, int page = 1)
        {
            var pageSize = 20;
            var records = FilterRecords(nameFilter, performerFilter);
            var pageRecords = records.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(records.Count(), page, pageSize);
            var viewRecords = pageRecords.ToList().Join(_dbContext.Performers.ToList(),
            e => e.PerformerId, t => t.Id,
            (e, t) => new RecordViewModel
            {
                Id = e.Id,
                PerformerName = t.Name,
                GenreId = e.GenreId,
                Album = e.Album,
                RecordDate = (e.RecordDate),
                Lasting = e.Lasting,
                Rating = e.Rating,
                ComposName = e.ComposName
            }).Join(_dbContext.Genres.ToList(), e => e.GenreId, t => t.Id,
            (e, t) => new RecordViewModel
            {
                Id = e.Id,
                PerformerName = e.PerformerName,
                GenreName = t.GenreName,
                Album = e.Album,
                RecordDate = e.RecordDate,
                Lasting = e.Lasting,
                Rating = e.Rating,
                ComposName = e.ComposName
            }
            );
            var pageItemsModel = new PageItemsModel<RecordViewModel> { Items = viewRecords, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        public IActionResult ResetFilter()
        {

            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("performerFilter");
            return RedirectToAction(nameof(Records));
        }

        public IActionResult ResetManageFilter()
        {

            HttpContext.Response.Cookies.Delete("nameFilter");
            HttpContext.Response.Cookies.Delete("performerFilter");
            return RedirectToAction(nameof(ManageRecords));
        }
        private IQueryable<Record> FilterRecords(string nameFilter, int? performerFilter)
        {
            IQueryable<Record> records = _dbContext.Records;
            nameFilter = nameFilter ?? HttpContext.Request.Cookies["nameFilter"];
            if (!string.IsNullOrEmpty(nameFilter))
            {
                records = records.Where(e => e.ComposName.Contains(nameFilter));
                HttpContext.Response.Cookies.Append("nameFilter", nameFilter);
            }
            int cookiePerformerFilter;
            int.TryParse(HttpContext.Request.Cookies["performerFilter"], out cookiePerformerFilter);
            performerFilter = performerFilter ?? cookiePerformerFilter;
            if (performerFilter != 0)
            {
                records = records.Where(e => e.PerformerId == performerFilter);
                HttpContext.Response.Cookies.Append("performerFilter", performerFilter.ToString());
            }

            if(performerFilter==0 && nameFilter == " ")
            {
                records = _dbContext.Records;
            }
            return records;
        }

       
        [Authorize(Roles = "Admin")]
        public ActionResult ManageRecords(string nameFilter, int? performerFilter,int page = 1)
        {
            var pageSize = 20;
            var records = FilterRecords(nameFilter, performerFilter);
            var pageRecords = records.ToList().OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(records.Count(), page, pageSize);
            var viewRecords = pageRecords.ToList().Join(_dbContext.Performers.ToList(),
            e => e.PerformerId, t => t.Id,
            (e, t) => new RecordViewModel
            {
                Id = e.Id,
                PerformerName = t.Name,
                GenreId = e.GenreId,
                Album = e.Album,
                RecordDate = (e.RecordDate),
                Lasting = e.Lasting,
                Rating = e.Rating,
                ComposName = e.ComposName
            }).Join(_dbContext.Genres.ToList(), e => e.GenreId, t => t.Id,
            (e, t) => new RecordViewModel
            {
                Id = e.Id,
                PerformerName = e.PerformerName,
                GenreName = t.GenreName,
                Album = e.Album,
                RecordDate = e.RecordDate,
                Lasting = e.Lasting,
                Rating = e.Rating,
                ComposName = e.ComposName
            }
            );
            var pageItemsModel = new PageItemsModel<RecordViewModel> { Items = viewRecords, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var record = _dbContext.Records.Find(id);
            if (record != null)
            {
                try
                {
                    _dbContext.Records.Remove(record);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home",
                    new { message = "Record contain related data and cannot be deleted" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Record not found" });
            }

            return RedirectToAction("ManageRecords");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new Record());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Record record)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Records.Add(record);
                _dbContext.SaveChanges();
                return RedirectToAction("ManageRecords");
            }

            return View(record);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var record = _dbContext.Records.Find(id);
            if (record != null)
            {
                return View(record);
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Record not found" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Record record)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Records.Update(record);
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return RedirectToAction(nameof(ManageRecords));
                }
            }

            return View(record);
        }
    }
}
