using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpArchContrib.Core.Logging
{
    public interface IExceptionLogger
    {
        void LogException(Exception err, bool isSilent, Type throwingType);
    }
}
