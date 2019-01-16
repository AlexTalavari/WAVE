using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using WAVE.Dal.Entities;

namespace WAVE.Website.Models
{
    public class MessagesModel : LayoutModel
    {
        public User ActiveUser;
        public List<ContactListItem> ContactList;
        public User CurrentUser;
        public List<UserMessage> Messages;

        public void Init(User user, User active)
        {
            InitContactList(user, active);
            if (active == null)
            {
                active = ContactList.First().User;
            }
            InitMessageList(user, active);
            CurrentUser = user;
            ActiveUser = active;
            Title = "Messages with " + active.GetFullName();
        }

        private void InitMessageList(User user, User active)
        {
            Messages = new List<UserMessage>();
            //var list1 = user.UserReceivedUserMessages//   Where(um => um.From == active);
            //var list2 = user.UserSendUserMessages.Where(um => um.To == active);
            // Messages.AddRange(list1);
            //Messages.AddRange(list2);
            Messages = Messages.DistinctBy(um => um.Id).ToList();
        }

        private void InitContactList(User user, User active)
        {
            ContactList = new List<ContactListItem>();
            //var list1 = user.UserReceivedUserMessages;
            //var results = from um in list1
            //              group um by um.From into g
            //              select new { User = g.Key, Messages = g.ToList(), Photo = g.First().From.ProfilePhoto.Url, Last = g.OrderByDescending(u => u.DateSent) };

            //var list2 = user.UserSendUserMessages;
            //var results2 = from um in list2
            //               group um by um.To into g
            //               select new { User = g.Key, Messages = g.ToList(), Photo = g.First().To.ProfilePhoto.Url, Last = g.OrderByDescending(u => u.DateSent) };
            //results2.Concat(results);
            //var results3 = from gum in results2
            //               group gum by gum.User into g
            //               select new { User = g.Key, Messages = g.ToList(), Photo = g.First().Photo, Last = g.OrderByDescending(u => u.Last.First().DateSent) };

            //foreach (var group in results3)
            //{
            //    var cli = new ContactListItem()
            //    {
            //        User = group.User,
            //        Status = group.Last.First().Last.First().Status,
            //        Date = group.Last.First().Last.First().DateSent
            //    };
            //    if (cli.User == active)
            //    {
            //        cli.Active = true;
            //    }
            //    ContactList.Add(cli);

            //}
        }
    }

    public class ContactListItem
    {
        public bool Active;
        public DateTime Date;
        public UserMessageStatus Status;
        public User User;
    }
}