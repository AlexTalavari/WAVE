using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class UserMessageMap : ClassMap<UserMessage>
    {

        public UserMessageMap()
        {
            Table("UserMessages");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Status);
            Map(x => x.DateSent);
            Map(x => x.Text);

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