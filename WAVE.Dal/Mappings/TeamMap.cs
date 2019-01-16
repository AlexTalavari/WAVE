using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            Table("Team");
            Id(x => x.Id);

            Map(x => x.Description).Nullable();
            Map(x => x.ExternalUrl).Nullable();
            Map(x => x.ImageUrl).Nullable();
            Map(x => x.Name).Nullable();
            Cache.ReadOnly();
        }
    }
}
