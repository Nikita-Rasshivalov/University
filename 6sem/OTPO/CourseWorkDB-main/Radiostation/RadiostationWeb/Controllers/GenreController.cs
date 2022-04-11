using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System.Linq;


namespace RadiostationWeb.Controllers
{
    public class GenreController : Controller
    {
        private readonly RadiostationWebDbContext _dbContext;
        public GenreController(RadiostationWebDbContext context)
        {
            _dbContext = context;
        }


        [Authorize]
        public ActionResult Genres(string genreNameFilter, int page = 1)
        {
            var pageSize = 10;
            var genres = FilterGenres(genreNameFilter);
            var pageGenres = genres.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(genres.Count(), page, pageSize);
            var viewGenres = pageGenres.ToList();

            var pageItemsModel = new PageItemsModel<Genre> { Items = viewGenres, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        public IActionResult ResetFilter()
        {

            HttpContext.Response.Cookies.Delete("genreNameFilter");
            return RedirectToAction(nameof(Genres));

        }
        private IQueryable<Genre> FilterGenres(string genreNameFilter)
        {
            IQueryable<Genre> genres = _dbContext.Genres;
            genreNameFilter = genreNameFilter ?? HttpContext.Request.Cookies["genreNameFilter"];
            if (!string.IsNullOrEmpty(genreNameFilter))
            {
                genres = genres.Where(e => e.Description.Contains(genreNameFilter));
                HttpContext.Response.Cookies.Append("genreNameFilter", genreNameFilter);
            }
            return genres;
        }

        [AuthorizeRoles(RoleType.Admin,RoleType.Employeе)]
        public ActionResult ManageGenres(int page = 1)
        {
            IQueryable<Genre> genres = _dbContext.Genres;
            var pageSize = 10;
            var pageGenres = genres.OrderByDescending(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(genres.Count(), page, pageSize);
            var viewGenres = pageGenres.ToList();
            var pageItemsModel = new PageItemsModel<Genre> { Items = viewGenres, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Delete(int id)
        {
            var genre = _dbContext.Genres.Find(id);
            if (genre != null)
            {
                try
                {
                    _dbContext.Genres.Remove(genre);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home",
                    new { message = "Genre contain related data and cannot be deleted" });
                }
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Genre not found" });
            }

            return RedirectToAction(nameof(ManageGenres));
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Create()
        {
            return View(new Genre());
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Genres.Add(genre);
                _dbContext.SaveChanges();
                RedirectToAction(nameof(ManageGenres));
            }

            return View(genre);
        }
        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        public ActionResult Edit(int id)
        {
            var genre = _dbContext.Genres.Find(id);
            if (genre != null)
            {
                return View(genre);
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { message = "Genre not found" });
            }
        }

        [AuthorizeRoles(RoleType.Admin, RoleType.Employeе)]
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Genres.Update(genre);
                if (_dbContext.SaveChanges() != 0)
                {
                    ViewData["SuccessMessage"] = "Information has been successfully edited";
                    return RedirectToAction(nameof(ManageGenres));
                }
            }

            return View(genre);
        }
    }
}
