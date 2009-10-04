using SharpArch.Core.PersistenceSupport;
using Tests.DomainModel.Entities;

namespace Tests.SharpArchContrib.PostSharp.NHibernate {
    public interface ITransactionTestProvider {
        IRepository<TestEntity> TestEntityRepository { get; set; }
        void InitTransactionManager();
        void Commit(string testEntityName);
        void CheckNumberOfEntities(int numberOfEntities);
        void DoCommit(string testEntityName);
        void DoCommitSilenceException(string testEntityName);
        void DoRollback();
        void DoNestedCommit();
        void DoNestedForceRollback();
        void DoNestedInnerForceRollback();
    }
}