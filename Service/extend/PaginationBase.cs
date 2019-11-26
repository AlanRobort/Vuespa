using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.extend
{
    public class PaginationBase
    {
        private int _PageSize = 10;

        public int PageIndex { get; set; } = 0;

        public int PageSize {
            get => _PageSize;
            set => _PageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        //private int MaxPagesize { get; set; } =100;
        private int MaxPageSize = 100;
    }
}
