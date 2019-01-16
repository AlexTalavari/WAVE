using System.Collections.Generic;
using WAVE.Dal.Entities;

namespace WAVE.Website.Models
{
    public class HomeModel : LayoutModel
    {
        public HomeModel()
        {
            Title = "Welcome";
        }

        public User User { get; set; }
        public List<Action> TopActions { get; set; }
        public List<User> TopUsers { get; set; }
    }
}