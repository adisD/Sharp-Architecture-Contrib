using System;
using NHibernate;
using PostSharp.Laos;
using SharpArch.Data.NHibernate;

namespace SharpArchContrib.Data.NHibernate
{
    /// <summary>
    /// Provides support for System.Transaction transactions
    /// </summary>
    [Serializable]
    public class NHibernateTransactionManager : TransactionManagerBase
    {
        public override void PushTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            base.PushTransaction(factoryKey, eventArgs);

            ITransaction transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            if (!transaction.IsActive)
            {
                transaction.Begin();
            }
        }

        public override bool TransactionIsActive(string factoryKey)
        {
            ITransaction transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            return transaction != null && transaction.IsActive;
        }

        public override void RollbackTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            ITransaction transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            if (TransactionDepth == 1 && transaction.IsActive)
            {
                transaction.Rollback();
            }
        }

        public override void CommitTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            ITransaction transaction = NHibernateSession.CurrentFor(factoryKey).Transaction;
            if (TransactionDepth == 1 && transaction.IsActive)
            {
                transaction.Commit();
            }
        }
    }
}