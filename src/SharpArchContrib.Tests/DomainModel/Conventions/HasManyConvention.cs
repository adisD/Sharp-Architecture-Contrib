using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Tests.DomainModel.Conventions {
    public class HasManyConvention : IHasManyConvention {
        #region IHasManyConvention Members

        public void Apply(IOneToManyCollectionInstance instance) {
            instance.Key.Column(instance.EntityType.Name + "Id");
        }

        #endregion
    }
}