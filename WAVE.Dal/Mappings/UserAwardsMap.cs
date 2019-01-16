using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class UserAwardMap : ClassMap<UserAward>
    {

        public UserAwardMap()
        {
            Table("UserAwards");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ImageUrl);
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.Description);

            References(x => x.To)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("UserId");
            Cache.ReadOnly();
        }
    }
}