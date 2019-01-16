namespace WAVE.Dal.Entities
{
    public class Rating : EntityBase
    {
       
        public virtual User User { get; set; }
        public virtual Action Action { get; set; }
        public virtual int Value { get; set; }
        public virtual string Text { get; set; }
    }

}