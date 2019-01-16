using System;

namespace WAVE.Dal.Entities
{
    public class ActionUpdate : EntityBase
    {
       
        public virtual Action Action { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual String Text { get; set; }

        public ActionUpdate()
        {            
            DateCreated = DateTime.Now;
            DateModified= DateTime.Now;
        }


    }

    
}