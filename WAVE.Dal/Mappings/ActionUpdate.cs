using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class ActionUpdateMap : ClassMap<ActionUpdate>
    {

        public ActionUpdateMap()
        {
            Table("ActionUpdates");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.Text);

            References(x => x.Action)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("ActionId");
            Cache.ReadOnly();
        }
    }
}