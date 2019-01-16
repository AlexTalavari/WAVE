namespace WAVE.Dal.Entities
{
    public class SuggestedAction : EntityBase
    {
       
        public virtual User User { get; set; }
        public virtual Action Action { get; set; }

        
    }

}