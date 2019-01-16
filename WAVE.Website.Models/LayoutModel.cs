using System;
using System.Collections.Generic;
using WAVE.Dal.Entities;

namespace WAVE.Website.Models
{
    public class LayoutModel
    {
        public LayoutModel()
        {
            Authenticated = false;
            SearchTerm = "";
        }

        public String Title { get; set; }
        public String SearchTerm { get; set; }
        public String NotificationMessage { get; set; }
        public List<Modal> Modals { get; set; }
        public User LoggedUser { get; set; }
        public bool Authenticated { get; set; }
        public string Ip { get; set; }
    }
}