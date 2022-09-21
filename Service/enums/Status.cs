using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.enums
{
    public enum Status
    {
        approve = 1,
        reject = 2,
        waitingforBA = 3,
        GotoDeveloper = 4,
        closed = 5,
        pending = 6,
        New = 7
    }
}
