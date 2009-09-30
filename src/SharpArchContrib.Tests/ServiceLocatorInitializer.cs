using System;
using Castle.Core;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.PersistenceSupport;
using SharpArchContrib.Core.Logging;
using SharpArchContrib.Data.NHibernate;

namespace Tests {
    public class ServiceLocatorInitializer {
        public static void Init() {
            Init(typeof(SystemTransactionManager));
        }

        public static void Init(Type transactionManagerType)
        {
            IWindsorContainer container = new WindsorContainer();
            container.AddComponent("TransactionManager", typeof(ITransactionManager),
                                   transactionManagerType);
            container.AddComponent("ExceptionLogger", typeof(IExceptionLogger), typeof(ExceptionLogger));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}