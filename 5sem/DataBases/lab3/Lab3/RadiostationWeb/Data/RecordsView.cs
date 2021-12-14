using System;
using System.Collections.Generic;

#nullable disable

namespace RadiostationWeb.Data
{
    public partial class RecordsView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GenreName { get; set; }
        public string Album { get; set; }
        public DateTime RecordDate { get; set; }
        public int Lasting { get; set; }
        public decimal Rating { get; set; }
        public string ComposName { get; set; }
    }
}
