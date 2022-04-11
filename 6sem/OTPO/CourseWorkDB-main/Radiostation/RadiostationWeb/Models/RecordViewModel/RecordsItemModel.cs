using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RadiostationWeb.Models
{
    public class RecordsItemModel
    {
        public IEnumerable<RecordViewModel> Items { get; set; }

        public PageViewModel PageModel { get; set; }

        public IEnumerable<SelectListItem> SelectPerformers { get; set; }
    }
}
