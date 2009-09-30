using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.PersistenceSupport;
using Tests.DomainModel.Entities;

namespace Tests.SharpArchContrib.Data.NHibernate
{
    public interface ITransactionTestProvider
    {
        void InitTransactionManager();
        IRepository<TestEntity> TestEntityRepository { get; set; }
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
