using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Tests.DomainModel.Conventions {
    public class PrimaryKeyConvention : IIdConvention {
        #region IConvention<IIdentityInspector,IIdentityInstance> Members

        public void Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
            instance.UnsavedValue("0");
            instance.GeneratedBy.Identity();
        }

        #endregion
    }
}