using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA.Models
{
    public class RestaurantQuery
    {
        public string SearchPhrase { set; get; }
        public int PageNumber { set; get; }
        public int PageSize { set; get; }
        public string SortBy { set; get; }

        public SortDirection SortDirection{ set; get; }
    }
}
