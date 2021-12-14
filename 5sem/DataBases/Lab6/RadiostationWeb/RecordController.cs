using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab6.Data;
using Lab6.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6.Controllers
{
    public class RecordController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public RecordController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IEnumerable<Record> Get()
        {
            var records = _dbContext.Records.ToList();
            return records;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Record record = _dbContext.Records.FirstOrDefault(x => x.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            else
            {
                var recordView = new Record
                {
                    Album = record.Album,
                    ComposName = record.ComposName,
                    GenreId=record.GenreId,
                    PerformerId=record.PerformerId,
                    Lasting=record.Lasting,
                    Rating=record.Rating
                };
                return new ObjectResult(recordView);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Record record)
        {
            if (record == null)
            {
                return BadRequest();
            }

            _dbContext.Records.Add(record);
            _dbContext.SaveChanges();
            return Ok(record);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Record record)
        {
            if (record == null)
            {
                return BadRequest();
            }
            if (!_dbContext.Records.Any(t => t.Id == record.Id))
            {
                return NotFound();
            }

            _dbContext.Records.Update(record);
            _dbContext.SaveChanges();
            return Ok(record);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Record record = _dbContext.Records.FirstOrDefault(t => t.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            _dbContext.Records.Remove(record);
            _dbContext.SaveChanges();
            return Ok(record);
        }
    }
}
