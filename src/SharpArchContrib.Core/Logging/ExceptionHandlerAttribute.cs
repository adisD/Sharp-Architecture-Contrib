using System;
using Microsoft.Practices.ServiceLocation;
using PostSharp.Laos;

namespace SharpArchContrib.Core.Logging {
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ExceptionHandlerAttribute : OnExceptionAspect
    {
        private IExceptionLogger exceptionLogger;

        public ExceptionHandlerAttribute()
        {
            IsSilent = false;
        }

        public bool IsSilent { get; set; }
        public object ReturnValue { get; set; }

        private IExceptionLogger ExceptionLogger
        {
            get
            {
                if (exceptionLogger == null)
                {
                    exceptionLogger = ServiceLocator.Current.GetInstance<IExceptionLogger>();
                }
                return exceptionLogger;
            }
        }

        public override void OnException(MethodExecutionEventArgs eventArgs)
        {
            ExceptionLogger.LogException(eventArgs.Exception, IsSilent, eventArgs.Instance.GetType());
            if (IsSilent)
            {
                eventArgs.FlowBehavior = FlowBehavior.Return;
                eventArgs.ReturnValue = ReturnValue;
            }
        }
    }
}