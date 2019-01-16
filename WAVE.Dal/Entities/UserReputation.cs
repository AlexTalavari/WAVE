using System;

namespace WAVE.Dal.Entities
{
    public class UserReputation : EntityBase
    {
       
        public virtual User To { get; set; }
        public virtual int Points { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual String Description { get; set; }

        public UserReputation()
        {            
            DateCreated = DateTime.Now;
            DateModified= DateTime.Now;
        }


    }

    
}