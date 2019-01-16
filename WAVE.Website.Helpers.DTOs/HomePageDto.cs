using System;

namespace WAVE.Website.Helpers.DTOs
{
    public class HomePageDto
    {
        public int Id { get; protected set; }
        public Uri ImageUrl { get; set; }
        public string Title { get; set; }
        //public Uri link1 { get; set; }
        //public Uri link2 { get; set; }
        public int RemainingDays { get; set; }
        public string CategoryClass { get; set; }
        public int MaxValueBar { get; set; }
        public int CurrentValueBar { get; set; }
        public string Description { get; set; }
        public Uri CreatorImageUrl { get; set; }
        public string CreatorFullName { get; set; }
        public double CreatorRating { get; set; }
        public string Location { get; set; }
    }
}