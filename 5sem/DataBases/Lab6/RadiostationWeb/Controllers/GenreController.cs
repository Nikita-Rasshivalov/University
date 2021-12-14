using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;

using System.Linq;


namespace RadiostationWeb.Controllers
{
    public class GenreController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public GenreController(BDLab1Context context)
        {
            _dbContext = context;
        }


        [Authorize]
        public ActionResult Genres(string genreNameFilter, int page = 1)
        {
            var pageSize = 20;
            var genres = FilterGenres(genreNameFilter);
            var pageGenres = genres.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
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


            if (genreNameFilter == " ")
            {
                genres = _dbContext.Genres;
            }
            return genres;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageGenres(int page = 1)
        {
            IQueryable<Genre> genres = _dbContext.Genres;
            var pageSize = 20;
            var pageGenres = genres.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(genres.Count(), page, pageSize);
            var viewGenres = pageGenres.ToList();
            var pageItemsModel = new PageItemsModel<Genre> { Items = viewGenres, PageModel = pageViewModel };
            return View(pageItemsModel);
        }

        [Authorize(Roles = "Admin")]
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

            return RedirectToAction("ManageGenres");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new Genre());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Genres.Add(genre);
                _dbContext.SaveChanges();
                return RedirectToAction("ManageGenres");
            }

            return View(genre);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
