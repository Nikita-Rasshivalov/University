﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public string PerformerName { get; set; }
        public string GenreName { get; set; }

        public string Album { get; set; }
        public DateTime RecordDate { get; set; }
        public int Lasting { get; set; }
        public decimal Rating { get; set; }
        public string ComposName { get; set; }

    }
}
