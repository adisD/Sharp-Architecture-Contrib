using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Data.NHibernate;

namespace SharpArchContrib.Data.NHibernate {
    public interface IUnitOfWorkSessionStorage : ISessionStorage
    {
        void EndUnitOfWork(bool closeSessions);
    }
}