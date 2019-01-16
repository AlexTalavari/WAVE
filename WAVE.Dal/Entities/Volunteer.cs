using System;

namespace WAVE.Dal.Entities
{
    public class Volunteer : EntityBase
    {
       
        public virtual User User { get; set; }
        public virtual Action Action { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual VolunteerType Type { get; set; }

        
    }

    public enum VolunteerType
    {
        //TODO: Add Types
        Option1,
        Option2
    }
}