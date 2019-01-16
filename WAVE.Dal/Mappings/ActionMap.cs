using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class ActionMap : ClassMap<Action>
    {

        public ActionMap()
        {
            Table("Actions");
            Id(x => x.Id);

            Map(x => x.Title).Nullable();
            Map(x => x.Description).Nullable().Length(4001);
            Map(x => x.StartDate).Nullable();
            Map(x => x.EndDate).Nullable();
            Map(x => x.MinValueBar).Nullable();
            Map(x => x.MaxValueBar).Nullable();
            Map(x => x.CurrentValueBar).Nullable();
            Map(x => x.Adress).Nullable();
            Map(x => x.DateCreated).Nullable();
            Map(x => x.DateModified).Nullable();
            Map(x => x.Latitude).Nullable();
            Map(x => x.Longtitude).Nullable();
            Map(x => x.Goal1Max).Nullable();
            Map(x => x.Goal1Description).Nullable();
            Map(x => x.Goal2Max).Nullable();
            Map(x => x.Goal2Description).Nullable();
            Map(x => x.Goal3Max).Nullable();
            Map(x => x.Goal3Description).Nullable();
            Map(x => x.TwitterHashtag).Nullable();
            Map(x => x.Location).Nullable();
            Map(x => x.IsSuggested).Nullable();

            References(x => x.Category)
             .Not.Nullable()
             .Cascade.SaveUpdate()
             .Column("CategoryId");


            References(x => x.Creator)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("CreatorId");

            References(x => x.BannerPhoto)
             .Not.Nullable()
             .Cascade.SaveUpdate()
             .Column("BannerPhotoId");


            References(x => x.ThumbnailPhoto)
                .Not.Nullable()
                .Cascade.SaveUpdate()
                .Column("ThumbnailPhotoId");

            HasMany(x => x.Campaigns)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("ActionId");
            HasMany(x => x.Keywords)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
            HasMany(x => x.ActionUpdates)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
            HasMany(x => x.Volunteers)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
            HasMany(x => x.Comments)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
           HasMany(x => x.Contributions)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
           HasMany(x => x.SuggestedActions)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
            HasMany(x => x.Ratings)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("ActionId");
            Cache.ReadOnly();
        }
    }
}