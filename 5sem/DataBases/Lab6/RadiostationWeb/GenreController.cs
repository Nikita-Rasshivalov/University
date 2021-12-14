using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab6.Data;
using Lab6.Models;

using System.Linq;
using System.Collections.Generic;

namespace Lab6.Controllers
{
    public class GenreController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public GenreController(BDLab1Context context)
        {
            _dbContext = context;
        }


         [HttpGet]
        public IEnumerable<Genre> Get()
        {
            var genres = _dbContext.Genres.ToList();
            return genres;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Genre genre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            else
            {
                var genreView = new Genre
                {
                    GenreName = genre.GenreName,
                    Description = genre.Description
                };
                return new ObjectResult(genreView);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
            return Ok(genre);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            if (!_dbContext.Genres.Any(t => t.Id == genre.Id))
            {
                return NotFound();
            }

            _dbContext.Genres.Update(genre);
            _dbContext.SaveChanges();
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Genre genre = _dbContext.Genres.FirstOrDefault(t => t.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
            return Ok(genre);
        }
    }
}
