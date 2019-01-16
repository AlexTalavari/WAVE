using WAVE.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WAVE.Website.Helpers.DTOs
{
    public class UserDataDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public Uri ImageUrl { get; set; } 
        public string AccountUserName { get; set; }

        public IList<UserMessage> UserSendUserMessages { get; protected set; }
        public IList<UserMessage> UserReceivedUserMessages { get; protected set; }

        public IList<UserNotification> UserNotifications { get; protected set; }
    }
}
