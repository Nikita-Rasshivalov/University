using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RadiostationWeb.Models
{
    public class PageItemsModel<T>
    {
        public IEnumerable<T> Items { get; set; }

        public PageViewModel PageModel { get; set; }
    }
}
