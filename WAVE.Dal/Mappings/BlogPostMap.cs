using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    class BlogPostMap : ClassMap<BlogPost>
    {
        public BlogPostMap()
        {
            Table("BlogPost");
            Id(x => x.Id);

            Map(x => x.ImageUrl).Nullable();
            Map(x => x.Content).Nullable();
            Map(x => x.DateCreated).Nullable();
            Map(x => x.DateModified).Nullable();
            Map(x => x.Title).Nullable();
            References(x => x.Creator)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("CreatorId");
            Cache.ReadOnly();
        }
    }
}
