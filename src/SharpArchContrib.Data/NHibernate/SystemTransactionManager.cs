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
    public class SystemTransactionManager : TransactionManagerBase {
        public override void PushTransaction(string factoryKey, MethodExecutionEventArgs eventArgs) {
            base.PushTransaction(factoryKey, eventArgs);

            //If this is a new transaction, we have to close the session,
            //start the transaction and then open the new session to 
            //associated the NHibernate session with the transaction.
            //This is usually not a high cost activity since the connection
            //will be pulled out of the connection pool
            ISession session = NHibernateSession.CurrentFor(factoryKey);
            bool newTransaction = !TransactionIsActive(factoryKey);
            if (newTransaction)
            {
                session.Disconnect();
            }
            eventArgs.InstanceTag = new TransactionScope();

            if (newTransaction)
            {
                session.Reconnect();
            }
        }

        public override bool TransactionIsActive(string factoryKey) {
            return Transaction.Current != null;
        }

        public override void PopTransaction(string factoryKey, MethodExecutionEventArgs eventArgs) {
            var transactionScope = (eventArgs.InstanceTag as TransactionScope);
            if (transactionScope != null) {
                transactionScope.Dispose();
                eventArgs.InstanceTag = null;
            }

            base.PopTransaction(factoryKey, eventArgs);
        }

        public override void RollbackTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
        }

        public override void CommitTransaction(string factoryKey, MethodExecutionEventArgs eventArgs)
        {
            var transactionScope = (eventArgs.InstanceTag as TransactionScope);
            if (transactionScope != null) {
                transactionScope.Complete();
            }
        }
    }
}