using System;
using System.Collections.Generic;

namespace WAVE.Dal.Entities
{
    public class Keyword : EntityBase
    {
       
        public virtual string Tag { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }

        public virtual ISet<Action> Actions { get; protected set; }

        public Keyword()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;

            Actions = new HashSet<Action>();
        }
    
    }
}