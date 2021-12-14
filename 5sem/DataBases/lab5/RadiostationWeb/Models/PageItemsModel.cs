using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Models
{
    public class PageItemsModel<T>
    {
        public IEnumerable<T> Items { get; set; }

        public PageViewModel PageModel { get; set; }
    }
}
