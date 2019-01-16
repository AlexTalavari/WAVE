using System;
using WAVE.Dal.Entities;

namespace WAVE.Website.Models
{
    public class UserModel : LayoutModel
    {
        public User CurrentUser { get; set; }

        public void Init(User user)
        {
            CurrentUser = user;
            Title = CurrentUser.GetFullName();
        }
    }

    public class UserEditModel : LayoutModel
    {
        //Name Surname DateOfBirth Phone Website Facebook Twitter Linkedin GooglePlus Youtube Youtube Description Adress ZipCode City Country
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
        public string Youtube { get; set; }
        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}