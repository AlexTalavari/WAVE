using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WAVE.Dal.Entities
{
    public class Team : EntityBase
    {
        
       
        public virtual  string Description { get; set; }
        public virtual String ImageUrl { get; set; }
        public virtual String Name { get; set; }
        public virtual String ExternalUrl { get; set; }
       
    }
}
