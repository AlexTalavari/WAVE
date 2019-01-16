using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class CampaignMap : ClassMap<Campaign>
    {

        public CampaignMap()
        {
            Table("Campaigns");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.NumberOfUsersToSee);
            Map(x => x.SelectedRegion);
            Map(x => x.SelectedCountry);
            Map(x => x.SelectedCity);

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