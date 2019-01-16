using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class RestrictedUserNamesMap : ClassMap<RestrictedUserNames>
    {

        public RestrictedUserNamesMap()
        {
            Table("RestrictedUserNames");
            Id(x => x.Id);

            Map(x => x.Data).Nullable();
            Cache.ReadOnly();
        }
    }
}