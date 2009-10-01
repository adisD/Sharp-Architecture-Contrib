using NHibernate.Exceptions;
using NUnit.Framework;

namespace Tests.SharpArchContrib.Data.NHibernate
{
    [TestFixture]
    public class TransactionTests : TransactionTestsBase
    {
        private const string TestEntityName = "TransactionTest";

        private ITransactionTestProvider[] transactionTestProviders = new ITransactionTestProvider[]
                                                                          {
                                                                              new SystemTransactionTestProvider(),
                                                                              new NHibernateTransactionTestProvider(),
                                                                              new SystemUnitOfWorkTestProvider(),
                                                                              new NHibernateUnitOfWorkTestProvider()
                                                                          };

        protected override void InitializeData()
        {
            base.InitializeData();
            transactionTestProviders = GenerateTransactionManagers();
        }

        private ITransactionTestProvider[] GenerateTransactionManagers()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                transactionTestProvider.TestEntityRepository = testEntityRepository;
            }

            return transactionTestProviders;
        }

        //Tests call Setup and TearDown manually for each iteration of the loop since
        //we want a clean database for each iteration.  We could use the parameterized
        //test feature of Nunit 2.5 but, unfortunately that doesn't work with all test runners (e.g. Resharper)

        [Test]
        public void MultipleOperations()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoCommit(TestEntityName);
                transactionTestProvider.CheckNumberOfEntities(1);

                transactionTestProvider.DoCommit(TestEntityName + "1");
                transactionTestProvider.CheckNumberOfEntities(2);
                TearDown();
            }
        }

        [Test]
        public void MultipleOperationsRollbackFirst()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoRollback();
                transactionTestProvider.CheckNumberOfEntities(0);

                transactionTestProvider.DoCommit(TestEntityName);
                transactionTestProvider.CheckNumberOfEntities(1);
                TearDown();
            }
        }

        [Test]
        public void MultipleOperationsRollbackLast()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoCommit(TestEntityName + "1");
                transactionTestProvider.CheckNumberOfEntities(1);

                transactionTestProvider.DoRollback();
                transactionTestProvider.CheckNumberOfEntities(1);
                TearDown();
            }
        }

        [Test]
        public void NestedCommit()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoNestedCommit();
                transactionTestProvider.CheckNumberOfEntities(2);
                TearDown();
            }
        }

        [Test]
        public void NestedForceRollback()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoNestedForceRollback();
                transactionTestProvider.CheckNumberOfEntities(0);
                TearDown();
            }
        }

        [Test]
        public void NestedInnerForceRollback()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoNestedInnerForceRollback();
                transactionTestProvider.CheckNumberOfEntities(0);
                TearDown();
            }
        }

        [Test]
        public void Rollback()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoRollback();
                transactionTestProvider.CheckNumberOfEntities(0);
                TearDown();
            }
        }

        [Test]
        public void RollsbackOnException()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoCommit(TestEntityName);
                transactionTestProvider.CheckNumberOfEntities(1);

                Assert.Throws<GenericADOException>(() => transactionTestProvider.DoCommit(TestEntityName));
                transactionTestProvider.CheckNumberOfEntities(1);
                TearDown();
            }
        }

        [Test]
        public void RollsbackOnExceptionWithSilentException()
        {
            foreach (ITransactionTestProvider transactionTestProvider in transactionTestProviders)
            {
                SetUp();
                transactionTestProvider.InitTransactionManager();
                transactionTestProvider.DoCommit(TestEntityName);
                transactionTestProvider.CheckNumberOfEntities(1);

                transactionTestProvider.DoCommitSilenceException(TestEntityName);
                transactionTestProvider.CheckNumberOfEntities(1);
                TearDown();
            }
        }
    }
}