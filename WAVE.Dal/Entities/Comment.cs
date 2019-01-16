using System;

namespace WAVE.Dal.Entities
{
    public class Comment : EntityBase
    {
       
        public virtual User User { get; set; }
        public virtual Action Action { get; set; }        
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual string Text { get; set; }

        public Comment()
        {            
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}