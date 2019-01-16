using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class UserNotificationMap : ClassMap<UserNotification>
    {

        public UserNotificationMap()
        {
            Table("UserNotifications");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Status);
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.ImageUrl);
            Map(x => x.Text);
            Map(x => x.Type);
            Map(x => x.Data);

            References(x => x.To)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("UserId");
            Cache.ReadOnly();
        }
    }
}