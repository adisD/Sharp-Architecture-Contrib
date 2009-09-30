using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Tests.DomainModel.Conventions {
    public class HasManyConvention : IHasManyConvention {
        #region IConvention<IOneToManyCollectionInspector,IOneToManyCollectionInstance> Members

        public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name + "Id");
        }

        #endregion
    }
}