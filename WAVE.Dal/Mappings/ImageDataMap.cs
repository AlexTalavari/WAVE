using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class ImageDataMap : ClassMap<ImageData>
    {

        public ImageDataMap()
        {
            Table("ImageData");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.Url);
            Map(x => x.Approved);
            Map(x => x.Filename);
            Map(x => x.Flagged);


            References(x => x.Owner)
             .Not.Nullable()
             .Cascade.SaveUpdate()
             .Column("OwnerId");
            Cache.ReadOnly();
        }
    }
}