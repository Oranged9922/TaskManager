using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TOTaskAggregate
{
    public enum TOTaskStatus
    {
        Open,
        Assigned,
        InProgress,
        Completed,
        Closed,
        Blocked
    }
}
