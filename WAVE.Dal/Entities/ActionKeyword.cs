namespace WAVE.Dal.Entities
{
    public class ActionKeyword : EntityBase
    {
       
        public virtual Action Action { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}