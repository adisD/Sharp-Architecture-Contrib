using System;

namespace SharpArchContrib.Data.NHibernate {
    [Serializable]
    public abstract class TransactionManagerBase : ITransactionManager {
        [ThreadStatic] private static int perThreadTransactionDepth;

        #region ITransactionManager Members

        public int TransactionDepth {
            get { return perThreadTransactionDepth; }
        }

        public virtual object PushTransaction(string factoryKey, object transactionState) {
            perThreadTransactionDepth++;
            return transactionState;
        }

        public abstract bool TransactionIsActive(string factoryKey);

        public virtual object PopTransaction(string factoryKey, object transactionState) {
            perThreadTransactionDepth--;
            return transactionState;
        }

        public abstract object RollbackTransaction(string factoryKey, object transactionState);
        public abstract object CommitTransaction(string factoryKey, object transactionState);

        #endregion
    }
}