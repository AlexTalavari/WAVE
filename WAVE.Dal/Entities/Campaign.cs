using System;

namespace WAVE.Dal.Entities
{
    public class Campaign : EntityBase
    {
       
        public virtual User User { get; set; }
        public virtual Action Action { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int NumberOfUsersToSee { get; set; }
        public virtual Region SelectedRegion { get; set; }
        public virtual Country SelectedCountry { get; set; }
        public virtual City SelectedCity { get; set; }
       
    }

    public enum Region
    {
        //TODO: Add more
        Europe,
        Africa
    }

    public enum Country
    {
        //TODO: Add more
        Greece,
        Usa
    }
    public enum City
    {
        //TODO: Add more
       Athens,
       Atlanta
    } 
}