using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class UniteMap : ClassMap<Unite>
    {

        public UniteMap()
        {
            Table("UnitedUsers");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Status);

            References(x => x.From)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("UserId");
            References(x => x.To)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("UnitedUserId");
            Cache.ReadOnly();
        }
    }
}