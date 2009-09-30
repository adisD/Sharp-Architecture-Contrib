using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace SharpArchContrib.Core.Logging
{
    public class ExceptionLogger : IExceptionLogger
    {
        #region IExceptionLogger Members

        public void LogException(Exception err, bool isSilent, Type throwingType)
        {
            ILog logger = LogManager.GetLogger(throwingType);
            logger.Error(err);
        }

        #endregion
    }
}
