using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class ActionKeywordMap : ClassMap<ActionKeyword>
    {

        public ActionKeywordMap()
        {
            Table("ActionKeywords");
            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.Action)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("ActionId");

            References(x => x.Keyword)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("KeywordId");
            Cache.ReadOnly();
        }
    }
}