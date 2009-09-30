using SharpArch.Core.PersistenceSupport;
using SharpArchContrib.Data.NHibernate;
using Tests.DomainModel.Entities;

namespace Tests.SharpArchContrib.Data.NHibernate {
    public class SystemTransactionTestProvider : TransactionTestProviderBase, ITransactionTestProvider {
        protected override string TestEntityName {
            get { return "TransactionTest"; }
        }

        #region ITransactionTestProvider Members

        [Transaction]
        public override void Commit(string testEntityName) {
            base.Commit(testEntityName);
        }

        [Transaction]
        public override void DoCommit(string testEntityName) {
            base.DoCommit(testEntityName);
        }

        [Transaction(IsExceptionSilent = true)]
        public override void DoCommitSilenceException(string testEntityName) {
            base.DoCommitSilenceException(testEntityName);
        }

        [Transaction]
        public override void DoRollback() {
            base.DoRollback();
        }

        [Transaction]
        public override void DoNestedCommit() {
            base.DoNestedCommit();
        }

        [Transaction]
        public override void DoNestedForceRollback() {
            base.DoNestedInnerForceRollback();
        }

        [Transaction]
        public override void DoNestedInnerForceRollback() {
            base.DoNestedInnerForceRollback();
        }

        public void InitTransactionManager() {
            ServiceLocatorInitializer.Init(typeof(SystemTransactionManager));
        }

        #endregion
    }
}