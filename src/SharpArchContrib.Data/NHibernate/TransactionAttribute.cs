using System;
using Microsoft.Practices.ServiceLocation;
using PostSharp.Extensibility;
using PostSharp.Laos;
using SharpArch.Data.NHibernate;
using SharpArchContrib.Core.Logging;

namespace SharpArchContrib.Data.NHibernate
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    [MulticastAttributeUsage(MulticastTargets.Method, AllowMultiple = true)]
    [Serializable]
    public class TransactionAttribute : OnMethodBoundaryAspect
    {
        private IExceptionLogger exceptionLogger;
        private ITransactionManager transactionManager;

        public TransactionAttribute()
        {
            FactoryKey = NHibernateSession.DefaultFactoryKey;
        }

        public string FactoryKey { get; set; }
        public bool IsExceptionSilent { get; set; }
        public object ReturnValue { get; set; }

        protected ITransactionManager TransactionManager
        {
            get
            {
                if (transactionManager == null)
                {
                    transactionManager = ServiceLocator.Current.GetInstance<ITransactionManager>();
                }
                return transactionManager;
            }
        }

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

        public override void OnEntry(MethodExecutionEventArgs eventArgs)
        {
            TransactionManager.PushTransaction(FactoryKey, eventArgs);
        }

        public override void OnException(MethodExecutionEventArgs eventArgs)
        {
            CloseUnitOfWork(eventArgs);
            if (!(eventArgs.Exception is AbortTransactionException))
            {
                ExceptionLogger.LogException(eventArgs.Exception, IsExceptionSilent, eventArgs.Method.DeclaringType);
            }
            if (TransactionManager.TransactionDepth == 0 &&
                (IsExceptionSilent || eventArgs.Exception is AbortTransactionException))
            {
                eventArgs.FlowBehavior = FlowBehavior.Return;
                eventArgs.ReturnValue = ReturnValue;
            }
        }

        public override void OnSuccess(MethodExecutionEventArgs eventArgs)
        {
            CloseUnitOfWork(eventArgs);
        }

        protected virtual void CloseUnitOfWork(MethodExecutionEventArgs eventArgs)
        {
            if (eventArgs.Exception == null)
            {
                NHibernateSession.CurrentFor(FactoryKey).Flush();
                TransactionManager.CommitTransaction(FactoryKey, eventArgs);
            }
            else
            {
                TransactionManager.RollbackTransaction(FactoryKey, eventArgs);
            }
            TransactionManager.PopTransaction(FactoryKey, eventArgs);
        }
    }
}