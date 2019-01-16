using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class SuggestedActionMap : ClassMap<SuggestedAction>
    {

        public SuggestedActionMap()
        {
            Table("SuggestedActions");
            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.User)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("UserId");
            References(x => x.Action)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("ActionId");
            Cache.ReadOnly();
        }
    }
}