using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexMummy.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBurials { get; set; }
        public int BurialsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int pageNum { get; set; }
        public long idSearchString { get; set; }
        public string GenderSearchString { get; set; }
        public string PreservationSearchString { get; set; }
        public string HeadDirectionSearchString { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalNumBurials / BurialsPerPage);
        
    }
}
