using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class RatingMap : ClassMap<Rating>
    {

        public RatingMap()
        {
            Table("Ratings");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Value);
            Map(x => x.Text);

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