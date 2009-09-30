using SharpArchContrib.Data.NHibernate;

namespace Tests.SharpArchContrib.Data.NHibernate {
    public class NHibernateUnitOfWorkTestProvider : TransactionTestProviderBase, ITransactionTestProvider {
        protected override string TestEntityName {
            get { return "NHibernateUnitOfWorkTest"; }
        }

        #region ITransactionTestProvider Members

        [UnitOfWork]
        public override void Commit(string testEntityName) {
            base.Commit(testEntityName);
        }

        [UnitOfWork]
        public override void DoCommit(string testEntityName) {
            base.DoCommit(testEntityName);
        }

        [Transaction(IsExceptionSilent = true)]
        public override void DoCommitSilenceException(string testEntityName) {
            base.DoCommitSilenceException(testEntityName);
        }

        [UnitOfWork]
        public override void DoRollback() {
            base.DoRollback();
        }

        [UnitOfWork]
        public override void DoNestedCommit() {
            base.DoNestedCommit();
        }

        [UnitOfWork]
        public override void DoNestedForceRollback() {
            base.DoNestedInnerForceRollback();
        }

        [UnitOfWork]
        public override void DoNestedInnerForceRollback() {
            base.DoNestedInnerForceRollback();
        }

        public void InitTransactionManager() {
            ServiceLocatorInitializer.Init(typeof(NHibernateTransactionManager));
        }

        #endregion
    }
}