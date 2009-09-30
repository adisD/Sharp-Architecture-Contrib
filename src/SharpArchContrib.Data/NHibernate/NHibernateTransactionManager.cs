using System;
using System.Transactions;
using NHibernate;
using PostSharp.Laos;
using SharpArch.Data.NHibernate;

namespace SharpArchContrib.Data.NHibernate {
    /// <summary>
    /// Provides support for System.Transaction transactions
    /// </summary>
    [Serializable]
    public class NHibernateTransactionManager : TransactionManagerBase {
        public override void PushTransaction(string factoryKey, MethodExecutionEventArgs eventArgs) {
            base.PushTransaction(factoryKey, eventArgs);

            var transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            if (!transaction.IsActive)
            {
                transaction.Begin();
            }
        }

        public override bool TransactionIsActive(string factoryKey) {
            var transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            return transaction != null && transaction.IsActive;
        }

        public override void RollbackTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            var transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            if (TransactionDepth == 1 && transaction.IsActive)
            {
                transaction.Rollback();
            }
        }

        public override void CommitTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            var transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            if (TransactionDepth == 1 && transaction.IsActive)
            {
                transaction.Commit();
            }
        }
    }
}