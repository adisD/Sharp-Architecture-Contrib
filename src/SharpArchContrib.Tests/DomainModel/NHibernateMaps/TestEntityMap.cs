using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Tests.DomainModel.Entities;

namespace Tests.DomainModel.NHibernateMaps
{
    public class TestEntityMap : IAutoMappingOverride<TestEntity>
    {
        #region IAutoMappingOverride<TestEntity> Members

        public void Override(AutoMapping<TestEntity> mapping)
        {
            mapping.Map(c => c.Name).Unique();
        }

        #endregion
    }
}