using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Common.DTO
{
    public class PagedResult<T>
    {

        public class PagingInfo
        {
            public int PageNo { get; set; }
            public int PageSize { get; set; }
            public int PageCount { get; set; }
            public long TotalRecordCount { get; set; }
        }

        public List<T> Data { get; set; }

        public PagingInfo Paging { get; set; }

        public PagedResult()
        {

        }
        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Data = new List<T>(items);
            Paging = new PagingInfo
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecordCount = totalRecordCount,
                PageCount = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0
            };
        }

    }
}
