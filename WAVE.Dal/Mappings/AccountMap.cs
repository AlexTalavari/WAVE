using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Table("Accounts");
            Id(x => x.Id);

            Map(x => x.UserName).Nullable();
            Map(x => x.Password).Nullable();
            Map(x => x.Email).Nullable().UniqueKey("Email");
            Map(x => x.IsOnline).Nullable();
            Cache.ReadOnly();
        }
    }
}
