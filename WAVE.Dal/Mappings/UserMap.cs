using FluentNHibernate.Mapping;
using WAVE.Dal.Entities;

namespace WAVE.Dal.Mappings
{
    public class UserMap : ClassMap<User>
    {

        public UserMap()
        {
            Table("Users");
            Id(x => x.Id);

            Map(x => x.Name).Nullable();
            Map(x => x.Surname).Nullable();
            Map(x => x.UserType).Nullable();
            Map(x => x.Description).Nullable();
            Map(x => x.DateCreated).Nullable();
            Map(x => x.DateModified).Nullable();
            Map(x => x.Phone).Nullable();
            Map(x => x.Country).Nullable();
            Map(x => x.DateOfBirth).Nullable();
            Map(x => x.Website).Nullable();
            Map(x => x.Facebook).Nullable();
            Map(x => x.Twitter).Nullable();
            Map(x => x.GooglePlus).Nullable();
            Map(x => x.Linkedin).Nullable();
            Map(x => x.Youtube).Nullable();
            Map(x => x.LastLongtitude).Nullable();
            Map(x => x.City).Nullable();
            Map(x => x.Balance).Nullable();
            Map(x => x.Skype).Nullable();
            Map(x => x.Adress).Nullable();
            Map(x => x.ZipCode).Nullable();
            Map(x => x.School).Nullable();
            Map(x => x.AgreedToTermsDate).Nullable();
            Map(x => x.Activated).Nullable();

            References(x => x.Terms)
              .Not.Nullable()
              .Cascade.SaveUpdate()
              .Column("TermsId");

            References(x => x.Account)
             .Not.Nullable()
             .Cascade.SaveUpdate()
             .Column("AccountId");

             References(x => x.ProfilePhoto)
              .Not.Nullable()
              .Cascade.SaveUpdate()
              .Column("ProfilePhotoId");

             References(x => x.BannerPhoto)
              .Not.Nullable()
              .Cascade.SaveUpdate()
              .Column("BannerPhotoId");

            HasMany(x => x.Actions).KeyColumn("CreatorId").Inverse().Cascade.All();
            

            HasMany(x => x.UserSendUnites)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.UserReceivedUnites)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("UnitedUserId");

            HasMany(x => x.UserSendUserMessages)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.UserReceivedUserMessages)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("UnitedUserId");

            HasMany(x => x.UserNotifications)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("UserId");
            HasMany(x => x.UserReputations)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Fetch.Join().KeyColumn("UserId");
            HasMany(x => x.UserAwards)
               .Cascade.AllDeleteOrphan()
               .Inverse()
               .Fetch.Join().KeyColumn("UserId");
            HasMany(x => x.Campaigns)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.Volunteers)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.Comments)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.Contributions)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.SuggestedActions)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            HasMany(x => x.Ratings)
                .Cascade.AllDeleteOrphan()
                .Fetch.Join()
                .Inverse().KeyColumn("UserId");
            Cache.ReadOnly();
        }
    }
}