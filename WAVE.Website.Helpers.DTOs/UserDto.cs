using System;
using System.Collections.Generic;
using WAVE.Dal.Entities;

namespace WAVE.Website.Helpers.DTOs
{
    public class UserDto
    {
        
        public  int Id { get; protected set; }
        public  string Name { get; set; }
        public  string Surname { get; set; }
        public  UserType UserType { get; set; }
        public  Uri ImageUrl { get; set; }
        public  string Description { get; set; }
        public  DateTime DateCreated { get; set; }
        public  DateTime DateModified { get; set; }
        public  string Phone { get; set; }
        public  string Country { get; set; }
        public  DateTime DateOfBirth { get; set; }
        public  Uri Website { get; set; }
        public  Uri Facebook { get; set; }
        public  Uri Twitter { get; set; }
        public  Uri GooglePlus { get; set; }
        public  Uri Youtube { get; set; }
        public  Uri Linkedin { get; set; }
        public  float LastLatitude { get; set; }
        public  float LastLongtitude { get; set; }
        public  string City { get; set; }
        public  Gender Gender { get; set; }
        public  float Balance { get; set; }
        public  string Skype { get; set; }
        public string AccountUserName { get; set; }

        public IList<Unite> UserSendUnites { get; protected set; }
        public IList<Unite> UserReceivedUnites { get; protected set; }


        public IList<UserMessage> UserSendUserMessages { get; protected set; }
        public IList<UserMessage> UserReceivedUserMessages { get; protected set; }


        public IList<UserNotification> UserNotifications { get; protected set; }

        public IList<UserReputation> UserReputations { get; protected set; }

        public IList<UserAward> UserAwards { get; protected set; }

        public IList<Campaign> Campaigns { get; protected set; }

        public IList<Volunteer> Volunteers { get; protected set; }

        public IList<Comment> Comments { get; protected set; }

        public IList<Contribution> Contributions { get; protected set; }

        public IList<SuggestedAction> SuggestedActions { get; protected set; }

        public IList<Rating> Ratings { get; protected set; }
    }
    
}