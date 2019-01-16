using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class VolunteerMap : ClassMap<Volunteer>
    {

        public VolunteerMap()
        {
            Table("Volunteers");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.StartDate);
            Map(x => x.Type);

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