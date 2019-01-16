using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class TermMap : ClassMap<Term>
    {

        public TermMap()
        {
            Table("Terms");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.Terms);
            Cache.ReadOnly();
        }
    }
}
