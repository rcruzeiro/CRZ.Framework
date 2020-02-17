using System;
using System.Collections.Generic;
using System.Linq;

namespace CRZ.Framework.Web.Http
{
    public class WaitAndRetryOptions
    {
        public IEnumerable<TimeSpan> WaitList { get; }

        public WaitAndRetryOptions(IEnumerable<TimeSpan> waitList)
        {
            if (waitList == null || !waitList.Any()) throw new ArgumentException(nameof(waitList));

            WaitList = waitList;
        }
    }
}
