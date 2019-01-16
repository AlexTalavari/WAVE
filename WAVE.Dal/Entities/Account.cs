namespace WAVE.Dal.Entities
{
    public interface IAccount
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
    }

    public class Account : EntityBase, IAccount
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsOnline { get; set; }
    }
}
