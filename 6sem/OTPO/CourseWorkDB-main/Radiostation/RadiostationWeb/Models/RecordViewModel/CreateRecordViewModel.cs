using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public class CreateRecordViewModel
    {
        public int Id { get; set; }
        [Required]
        public int PerformerId { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public string Album { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }
        [Required]
        [Range(60, 600, ErrorMessage = "The value must be in the limit from 60 to 600")]
        public int Lasting { get; set; }
        [Required]
        [Range(1.0, 5.0, ErrorMessage = "The value must be in the limit from 1.0 to 5.0")]
        public decimal Rating { get; set; }
        [Required]
        public string СompositionName { get; set; }

        public IEnumerable<SelectListItem> PerformersList { get; set; }
        public IEnumerable<SelectListItem> GenresList { get; set; }

    }
}
