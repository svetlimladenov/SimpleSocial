using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Common.Logging
{
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.loglevel?view=dotnet-plat-ext-5.0
    public enum SimpleSocialLogLevel
    {
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        None = 6
    }
}
