using System;
using PostSharp.Laos;

namespace SharpArchContrib.Data.NHibernate
{
    [Serializable]
    public abstract class TransactionManagerBase : ITransactionManager
    {
        [ThreadStatic] private static int perThreadTransactionDepth;

        #region ITransactionManager Members

        public int TransactionDepth
        {
            get { return perThreadTransactionDepth; }
        }

        public virtual void PushTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            perThreadTransactionDepth++;
        }

        public abstract bool TransactionIsActive(string factoryKey);

        public virtual void PopTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            perThreadTransactionDepth--;
        }

        public abstract void RollbackTransaction(string factoryKey, MethodExecutionEventArgs eventArgs);
        public abstract void CommitTransaction(string factoryKey, MethodExecutionEventArgs eventArgs);

        #endregion
    }
}