using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }
        public PagedResult(List<T> items, int totalCount, int pageSize, int pageNumer)
        {
            Items = items;
            TotalPages = totalCount;
            ItemsFrom = pageSize * (pageNumer -1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            totalCount = (int)Math.Ceiling(totalCount/(double) pageSize);
        }

    }
}
