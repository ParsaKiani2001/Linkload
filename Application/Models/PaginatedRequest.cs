using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PaginatedRequest
    {
        private int _pageIndex;
        private int _pageSize;
        public int PageSize
        {
            get
            { return _pageSize == 0 ? 10 : _pageSize; }
            set { _pageSize = value; }
        }
        public int PageIndex { get { return _pageIndex == 0 ? 1 : _pageIndex; } set { _pageIndex = value; } }
    }
}
