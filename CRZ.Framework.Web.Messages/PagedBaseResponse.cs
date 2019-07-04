using System;
using System.Collections.Generic;
using System.Linq;

namespace CRZ.Framework.Web.Messages
{
    public abstract class PagedBaseResponse<T> : BaseResponse<T>
        where T : class
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 1;

        public int TotalCount
        {
            get
            {
                if (Data is IEnumerable<T> list)
                    return list.Count();

                return 1;
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Round((decimal)TotalCount / PageSize);
            }
        }
    }
}
