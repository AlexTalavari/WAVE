using System;

namespace WAVE.Dal.Entities
{
    public class Term : EntityBase
    {
       
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual String Terms { get; set; }

    }


}