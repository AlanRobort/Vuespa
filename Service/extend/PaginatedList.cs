using System;
using System.Collections.Generic;
using System.Text;

namespace Service.extend
{
   public  class PaginatedList<T>:List<T> where T:class
    {
        public PaginationBase paginationBase { get; set; }

        //数据总的个数
            //3
        public int TotalItemCount { get; set; }
        //分页数
        //
        public int PageCount => TotalItemCount / paginationBase.PageSize + (TotalItemCount % paginationBase.PageSize > 0 ? 1 : 0);
        //上一页
        public bool HasPrevious => paginationBase.PageIndex > 0;

        //下一页
        public bool HasNext => paginationBase.PageIndex < PageCount - 1;

        public PaginatedList(int pageIndex,int totalItemCount,int pageSize,IEnumerable<T> data)
        {
            paginationBase = new PaginationBase
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            TotalItemCount = totalItemCount;
            AddRange(data);

        }

    }
}
