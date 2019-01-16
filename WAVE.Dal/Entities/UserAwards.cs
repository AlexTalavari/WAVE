using System;

namespace WAVE.Dal.Entities
{
    public class UserAward : EntityBase
    {
        public virtual User To { get; set; }
        public virtual Uri ImageUrl { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual String Description { get; set; }

        public UserAward()
        {            
            DateCreated = DateTime.Now;
            DateModified= DateTime.Now;
        }


    }

    
}