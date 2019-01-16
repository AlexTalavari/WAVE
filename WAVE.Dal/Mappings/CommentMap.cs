using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class CommentMap : ClassMap< Comment>
    {

        public CommentMap()
        {
            Table("Comments");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
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