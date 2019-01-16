using System;

namespace WAVE.Dal.Entities
{
    public class Category : EntityBase
    {
       
        public virtual string Title { get; set; }
        public virtual string Abreviation { get; set; }
        public virtual string ShortTitle { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }

        public virtual string SmallImageUrl { get; set; }

        public Category()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    
    }
}