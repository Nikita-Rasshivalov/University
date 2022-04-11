using RadiostationWeb.Data;


namespace RadiostationWeb.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState SurnameSort { get; set; }    
        public SortState RecordSort { get; set; }
        public SortState DateSort { get; set; }
        
        public SortState Current { get; set; }     
        public bool Up { get; set; }  

        public SortViewModel(SortState sortOrder)
        {
            NameSort = SortState.NameAsc;
            SurnameSort = SortState.SurnameAsc;
            RecordSort = SortState.RecorNameAsc;
            DateSort = SortState.DateAsc;
            Up = true;

            if (sortOrder == SortState.NameDsc 
                || sortOrder == SortState.SurnameDsc
                || sortOrder == SortState.RecordNameDsc
                || sortOrder == SortState.DateDsc)

            {
                Up = false;
            }

            Current = sortOrder switch
            {
                SortState.NameDsc => NameSort = SortState.NameAsc,
                SortState.NameAsc => NameSort = SortState.NameDsc,
                SortState.SurnameDsc => SurnameSort = SortState.SurnameAsc,
                SortState.SurnameAsc => SurnameSort = SortState.SurnameDsc,
                SortState.RecordNameDsc => RecordSort = SortState.RecorNameAsc,
                SortState.RecorNameAsc => RecordSort = SortState.RecordNameDsc,
                SortState.DateAsc => DateSort = SortState.DateDsc,
                _ => DateSort = SortState.DateAsc,
            };
        }
    }
}
