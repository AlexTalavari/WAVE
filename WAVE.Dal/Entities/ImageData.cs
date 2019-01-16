using System;

namespace WAVE.Dal.Entities
{
    public class ImageData : EntityBase
    {
               
        public virtual String Url { get; set; }

        public virtual String Filename { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual User Owner { get; set; }
        public virtual bool Approved { get; set; }
        public virtual bool Flagged { get; set; }
        public ImageData()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    
    }
}