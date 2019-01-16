using System;
using System.Collections.Generic;

namespace WAVE.Dal.Entities
{
    [Serializable]
    public class User : EntityBase
    {
       
        public virtual Account Account { get; set; }

        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual String UserType { get; set; }
        public virtual ImageData ProfilePhoto { get; set; }
        public virtual ImageData BannerPhoto { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Country { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual String Website { get; set; }
        public virtual String Facebook { get; set; }
        public virtual String Twitter { get; set; }
        public virtual String GooglePlus { get; set; }
        public virtual String Youtube { get; set; }
        public virtual String Linkedin { get; set; }
        public virtual float LastLatitude { get; set; }
        public virtual float LastLongtitude { get; set; }
        public virtual string City { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual float Balance { get; set; }
        public virtual string Skype { get; set; }
        public virtual string Adress { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string School { get; set; }
        public virtual DateTime AgreedToTermsDate { get; set; }
        public virtual Term Terms { get; set; }
        public virtual bool Activated { get; set; }

        public virtual ISet<Unite> UserSendUnites { get; protected set; }
        public virtual ISet<Unite> UserReceivedUnites { get; protected set; }


        public virtual ISet<UserMessage> UserSendUserMessages { get; protected set; }
        public virtual ISet<UserMessage> UserReceivedUserMessages { get; protected set; }

        
        public virtual ISet<UserNotification> UserNotifications { get; protected set; }

        public virtual ISet<UserReputation> UserReputations { get; protected set; }

        public virtual ISet<UserAward> UserAwards { get; protected set; }

        public virtual ISet<Campaign> Campaigns { get; protected set; }

        public virtual IList<Volunteer> Volunteers { get; protected set; }

        public virtual ISet<Comment> Comments { get; protected set; }

        public virtual ISet<Contribution> Contributions { get; protected set; }

        public virtual ISet<SuggestedAction> SuggestedActions { get; protected set; }


        public virtual ISet<Rating> Ratings { get; protected set; }

        public virtual ISet<Action> Actions { get; set; }

        public User()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;

            UserSendUnites = new HashSet<Unite>();
            UserReceivedUnites = new HashSet<Unite>();

            UserSendUserMessages = new HashSet<UserMessage>();
            UserReceivedUserMessages = new HashSet<UserMessage>();

            UserNotifications = new HashSet<UserNotification>();

            UserReputations = new HashSet<UserReputation>();

            UserAwards = new HashSet<UserAward>();

            Campaigns = new HashSet<Campaign>();

            Volunteers = new List<Volunteer>();

            Comments = new HashSet<Comment>();

            Contributions = new HashSet<Contribution>();

            SuggestedActions = new HashSet<SuggestedAction>();

            Ratings = new HashSet<Rating>();
        }

        public virtual bool AddUnite(User to)
        {
            try
            {
                var unite = new Unite {From = this, To = to};
                UserSendUnites.Add(unite);
                to.UserReceivedUnites.Add(unite);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual bool AddMessage(User to, String messageText)
        {
            try
            {
                var message = new UserMessage {Text = messageText, From = this, To = to};
                UserSendUserMessages.Add(message);
                to.UserReceivedUserMessages.Add(message);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual bool AddNotification(UserNotification notification)
        {
            try
            {
                UserNotifications.Add(notification);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual bool AddReputation(UserReputation reputation)
        {
            try
            {
                UserReputations.Add(reputation);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual bool AddAward(UserAward award)
        {
            try
            {
                UserAwards.Add(award);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual double GetRating()
        {
            double sum = 0.0;
            int counter=0;

            foreach (Action a in Actions) 
            {
                foreach (Rating r in a.Ratings) 
                {
                    sum += r.Value;
                    counter++;
                }
            }
            if (counter != 0)
            {
                return sum / counter;
            }
            return -1;
        }

        public virtual string GetFullName() 
        {
            return Name + " " + Surname;
        }
        public virtual bool IsVolunteer(Action action) 
        {
            var list =new List<Volunteer>(Volunteers);
            if (list.Exists(v => v.Action.Id == action.Id))
                return true;
            else
                return false;
        }
        public virtual bool IsUnited(User user)
        {
            foreach (Unite un in UserSendUnites) 
            {
                if (un.To == user)
                    return true;
            }
            return false;
        }


       
    }
    public enum UserType
    {
        Volunteer,
        Group,
        Ngo,
        Company,
        Guest
    }
    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}