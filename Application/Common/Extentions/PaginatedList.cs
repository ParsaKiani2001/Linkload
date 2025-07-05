using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extentions
{
    public class PaginatedList<T>
    {
        public List<T> List { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
    }
}
