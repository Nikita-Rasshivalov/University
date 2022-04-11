using System.Collections.Generic;


namespace RadiostationWeb.Models
{
    public class BroadcastsItemModel
    {
        public IEnumerable<BroadcastViewModel> Items { get; set; }

        public PageViewModel PageModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}
