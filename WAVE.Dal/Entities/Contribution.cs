using System;

namespace WAVE.Dal.Entities
{
    public class Contribution : EntityBase
    {
       
        public virtual User User { get; set; }
        public virtual Action Action { get; set; }
        public virtual double Amount { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual ContributionType Type { get; set; }
        public virtual Currency Currency { get; set; }

        public Contribution()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }

    public enum ContributionType
    {
        //TODO: Add Types
        Option1,
        Option2
    }

    public enum Currency
    {
        //TODO: Add Types
        Option1,
        Option2
    }
}