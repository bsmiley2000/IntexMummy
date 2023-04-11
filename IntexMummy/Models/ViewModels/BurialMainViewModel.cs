using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntexMummy.Models.ViewModels;

namespace IntexMummy.Models
{
    public class BurialMainViewModel
    {
        public IEnumerable<Burialmain> Burialmain { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
