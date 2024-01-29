using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHat.Common.Static.Enum
{
    public enum ServiceResponseTypes : short
    {
        SUCCESS,
        BADPARAMETERS,
        ERROR,
        TIMEOUT,
        UNKNOWN,
        UNAUTHORIZED,
        THIRDPARTYERROR,
        BADREQUEST,
        NOTFOUND
    }
}
