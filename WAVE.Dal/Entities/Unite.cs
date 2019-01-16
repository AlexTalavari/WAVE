namespace WAVE.Dal.Entities
{
    public class Unite : EntityBase
    {
       
        public virtual User From { get; set; }
        public virtual User To { get; set; }
        public virtual UniteStatus Status { get; set; }

        public Unite()
        {
           Status = UniteStatus.Pending;
        }
    }

    public enum UniteStatus
    {
        Accepted,
        Denied,
        Pending
    } 
}