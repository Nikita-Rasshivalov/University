
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace RadiostationWeb.Models
{
    public class PerformersItemsModel
    {
        public IEnumerable<PerformerViewModel> Items { get; set; }

        public PageViewModel PageModel { get; set; }
        public IEnumerable<SelectListItem> SelectGroups { get; set; }
    }
}
