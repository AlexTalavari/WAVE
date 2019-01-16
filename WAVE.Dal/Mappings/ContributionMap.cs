using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class ContributionMap : ClassMap<Contribution>
    {

        public ContributionMap()
        {
            Table("Contributions");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DateCreated);
            Map(x => x.Type);
            Map(x => x.DateModified);
            Map(x => x.Amount);
            Map(x => x.Currency);

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