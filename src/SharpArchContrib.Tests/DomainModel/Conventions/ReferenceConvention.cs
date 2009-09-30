using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Tests.DomainModel.Conventions {
    public class ReferenceConvention : IReferenceConvention {
        #region IConvention<IManyToOneInspector,IManyToOneInstance> Members

        public void Apply(FluentNHibernate.Conventions.Instances.IManyToOneInstance instance)
        {
            instance.Column(instance.Property.Name + "Id");
        }

        #endregion
    }
}