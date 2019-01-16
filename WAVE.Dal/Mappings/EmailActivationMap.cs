using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class EmailActivationMap : ClassMap<EmailActivation>
    {

        public EmailActivationMap()
        {
            Table("EmailActivation");
            Id(x => x.Id);

            Map(x => x.Email).Nullable();
            Map(x => x.Code).Nullable();
            Cache.ReadOnly();
        }
    }
}