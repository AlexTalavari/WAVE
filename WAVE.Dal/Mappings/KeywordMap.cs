using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class KeywordMap : ClassMap<Keyword>
    {

        public KeywordMap()
        {
            Table("Keywords");
            Id(x => x.Id);

            Map(x => x.Tag).Nullable();
            Map(x => x.DateCreated).Nullable();
            Map(x => x.DateModified).Nullable();


            HasMany(x => x.Actions)
            .Cascade.AllDeleteOrphan()
            .Inverse()
            .Fetch.Join().KeyColumn("KeywordId");
            Cache.ReadOnly();
        }
    }
}