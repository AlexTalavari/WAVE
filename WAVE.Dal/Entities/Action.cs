using System;
using System.Collections.Generic;


namespace WAVE.Dal.Entities
{
    public class Action : EntityBase
    {
       
        public virtual string Title { get; set; }
        public virtual User Creator { get; set; } 
        public virtual string Description { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int MinValueBar { get; set; }
        public virtual int MaxValueBar { get; set; }
        public virtual int CurrentValueBar { get; set; }
        public virtual string Adress { get; set; }
        public virtual ImageData ThumbnailPhoto { get; set; }
        public virtual ImageData BannerPhoto { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longtitude { get; set; }
        public virtual int Goal1Max { get; set; }
        public virtual string Goal1Description { get; set; }
        public virtual int Goal2Max { get; set; }
        public virtual string Goal2Description { get; set; }
        public virtual int Goal3Max { get; set; }
        public virtual string Goal3Description { get; set; }
        public virtual string TwitterHashtag { get; set; }
        public virtual bool IsSuggested { get; set; }

        public virtual Category Category { get; set; }

        public virtual string Location { get; set; }

        public virtual ISet<ActionUpdate> ActionUpdates { get; protected set; }

        public virtual ISet<Keyword> Keywords { get; protected set; }

        public virtual ISet<Campaign> Campaigns { get; protected set; }

        public virtual ISet<Volunteer> Volunteers { get; protected set; }

        public virtual ISet<Comment> Comments { get; protected set; }

        public virtual ISet<Contribution> Contributions { get; protected set; }

        public virtual ISet<SuggestedAction> SuggestedActions { get; protected set; }

        public virtual ISet<Rating> Ratings { get; protected set; }

        public Action()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;

            ActionUpdates = new HashSet<ActionUpdate>();

            Keywords = new HashSet<Keyword>();

            Campaigns = new HashSet<Campaign>();

            Volunteers = new HashSet<Volunteer>();

            Comments = new HashSet<Comment>();

            Contributions = new HashSet<Contribution>();

            SuggestedActions = new HashSet<SuggestedAction>();

            Ratings = new HashSet<Rating>();


        }



        public virtual bool AddActionUpdate(ActionUpdate actionUpdate)
        {
            try
            {
                ActionUpdates.Add(actionUpdate);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual bool AddKeyword(Keyword keyword)
        {
            try
            {
                Keywords.Add(keyword);
                return true;
            }
            catch (Exception)
            {
                //TODO: Add Logging
                return false;
            }
        }

        public virtual int GetRemainingDays() 
        {
            return StartDate.Subtract(DateTime.Now).Days;
        }


        public virtual string GetCategoryClass() 
        {
            return Category.ShortTitle;
        }
        public virtual string GetStatus()
        {
            if (GetRemainingDays() > 0) 
            {
                return "Ongoing";
            }
            else if (Goal1Max <= Volunteers.Count)
            {
                return "Successfull";
            }
            else 
            {
                return "Unsuccessfull";
            }
        }
        
    }
}