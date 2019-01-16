using System;
using NHibernate;

namespace WAVE.Dal.Entities
{
    public class UserNotification : EntityBase
    {
       
        public virtual User To { get; set; }
        public virtual UserNotificationStatus Status { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual String Text { get; set; }
        public virtual Uri ImageUrl{ get; set; }
        public virtual UserNotificationType Type { get; set; }
        public virtual String Data { get; set; }

        public UserNotification()
        {
            Status = UserNotificationStatus.Unread;
            DateCreated = DateTime.Now;
            DateModified= DateTime.Now;
        }
    }

    public enum UserNotificationStatus
    {
        Read,
        Deleted,
        Unread
    }

    public enum UserNotificationType
    {
        Follow,
        Attend,
        Comment
    } 
}