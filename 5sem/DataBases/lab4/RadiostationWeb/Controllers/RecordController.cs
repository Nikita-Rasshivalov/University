using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
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

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Records()
        {
            var records = _dbContext.Records.ToList();
            var viewRecords = records.Join(_dbContext.Performers.ToList(),
                e => e.PerformerId, t => t.Id,
                (e, t) => new RecordViewModel
                {
                    Id = e.Id,
                    PerformerName = t.Name,
                    GenreId = e.GenreId,
                    Album = e.Album,
                    RecordDate = e.RecordDate,
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
            return View(viewRecords);

        }
    }
}
