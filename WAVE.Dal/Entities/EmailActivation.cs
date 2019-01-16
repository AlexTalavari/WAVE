using System;

namespace WAVE.Dal.Entities
{

    public class EmailActivation : EntityBase
    {
       
        public virtual string Email { get; set; }
        public virtual Guid Code { get; set; }
        public virtual bool Activated { get; set; }

        public EmailActivation() { }
    }
}
