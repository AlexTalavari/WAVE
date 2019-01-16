using System;

namespace WAVE.Dal.Entities
{
    public class UserMessage : EntityBase
    {
        public virtual User From { get; set; }
        public virtual User To { get; set; }
        public virtual UserMessageStatus Status { get; set; }
        public virtual DateTime DateSent { get; set; }
        public virtual String Text { get; set; }

        public UserMessage()
        {
            Status = UserMessageStatus.Unread;
            DateSent = DateTime.Now;
        }
    }

    public enum UserMessageStatus
    {
        Read,
        Deleted,
        Unread
    } 
}