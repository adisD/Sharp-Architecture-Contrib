using System;
using PostSharp.Extensibility;
using PostSharp.Laos;
using SharpArch.Data.NHibernate;
using SharpArchContrib.Data.NHibernate;

namespace SharpArchContrib.PostSharp.NHibernate {
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    [MulticastAttributeUsage(MulticastTargets.Method, AllowMultiple = true)]
    public sealed class UnitOfWorkAttribute : TransactionAttribute {
        public UnitOfWorkAttribute() {
            CloseSessions = true;
        }

        public bool CloseSessions { get; set; }

        protected override object CloseUnitOfWork(MethodExecutionEventArgs eventArgs) {
            object transactionState = base.CloseUnitOfWork(eventArgs);
            if (TransactionManager.TransactionDepth == 0) {
                var sessionStorage = (NHibernateSession.Storage as IUnitOfWorkSessionStorage);
                if (sessionStorage != null) {
                    sessionStorage.EndUnitOfWork(CloseSessions);
                }
            }

            return transactionState;
        }
    }
}