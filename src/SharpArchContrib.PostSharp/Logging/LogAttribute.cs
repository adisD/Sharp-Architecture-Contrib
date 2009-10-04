using System;
using System.Reflection;
using System.Text;
using log4net;
using Microsoft.Practices.ServiceLocation;
using PostSharp.Extensibility;
using PostSharp.Laos;
using SharpArchContrib.Core.Logging;

namespace SharpArchContrib.PostSharp.Logging {
    [Serializable]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,
        Inherited = false)]
    [MulticastAttributeUsage(
        MulticastTargets.Method | MulticastTargets.InstanceConstructor | MulticastTargets.StaticConstructor,
        AllowMultiple = true)]
    public class LogAttribute : OnMethodBoundaryAspect {
        private IExceptionLogger exceptionLogger;

        public LogAttribute() {
            EntryLevel = LoggingLevel.Debug;
            SuccessLevel = LoggingLevel.Debug;
            ExceptionLevel = LoggingLevel.Error;
        }

        public LoggingLevel EntryLevel { get; set; }
        public LoggingLevel SuccessLevel { get; set; }
        public LoggingLevel ExceptionLevel { get; set; }

        private IExceptionLogger ExceptionLogger {
            get {
                if (exceptionLogger == null) {
                    exceptionLogger = ServiceLocator.Current.GetInstance<IExceptionLogger>();
                }
                return exceptionLogger;
            }
        }

        public override void OnEntry(MethodExecutionEventArgs eventArgs) {
            ILog logger = LogManager.GetLogger(eventArgs.Method.DeclaringType);
            if (ShouldLog(logger, EntryLevel, eventArgs)) {
                var logMessage = new StringBuilder();
                logMessage.Append(string.Format("{0}(", eventArgs.Method.Name));

                object[] argumentValues = eventArgs.GetReadOnlyArgumentArray();
                ParameterInfo[] parameterInfos = eventArgs.Method.GetParameters();
                if (argumentValues != null && parameterInfos != null) {
                    for (int i = 0; i < argumentValues.Length; i++) {
                        if (i > 0) {
                            logMessage.Append(" ");
                        }
                        logMessage.Append(string.Format("{0}:[{1}]", parameterInfos[i].Name, argumentValues[i]));
                    }
                }
                logMessage.Append(")");
                logger.Log(EntryLevel, logMessage.ToString());
            }
        }

        public override void OnSuccess(MethodExecutionEventArgs eventArgs) {
            ILog logger = LogManager.GetLogger(eventArgs.Method.DeclaringType);
            if (ShouldLog(logger, SuccessLevel, eventArgs)) {
                logger.Log(SuccessLevel,
                           string.Format("{0} Returns:[{1}]", eventArgs.Method.Name,
                                         eventArgs.ReturnValue != null ? eventArgs.ReturnValue.ToString() : ""));
            }
        }

        public override void OnException(MethodExecutionEventArgs eventArgs) {
            ILog logger = LogManager.GetLogger(eventArgs.Method.DeclaringType);
            if (ShouldLog(logger, ExceptionLevel, eventArgs)) {
                ExceptionLogger.LogException(eventArgs.Exception, false, eventArgs.Method.DeclaringType);
            }
        }

        private bool ShouldLog(ILog logger, LoggingLevel loggingLevel, MethodExecutionEventArgs args) {
            if (args != null && args.Method != null && args.Method.Name != null) {
                return logger.IsEnabledFor(loggingLevel);
            }

            return false;
        }
    }
}