using System;

namespace WAVE.Dal.Entities
{
    public class BlogPost : EntityBase
    {
        
       
        public virtual  string Content { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual String ImageUrl { get; set; }
        public virtual User Creator { get; set; }

        public virtual String Title { get; set; }
        public BlogPost()
        {            
            DateCreated = DateTime.Now;
            DateModified= DateTime.Now;
        }
    }
}
