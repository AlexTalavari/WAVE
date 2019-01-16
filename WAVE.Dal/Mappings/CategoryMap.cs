using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {

        public CategoryMap()
        {
            Table("Categories");
            Id(x => x.Id);

            Map(x => x.Title).Nullable();
            Map(x => x.Abreviation).Nullable();
            Map(x => x.ShortTitle).Nullable();
            Map(x => x.DateCreated).Nullable();
            Map(x => x.DateModified).Nullable();
            Map(x => x.SmallImageUrl).Nullable();
            Cache.ReadOnly();
        }
    }
}