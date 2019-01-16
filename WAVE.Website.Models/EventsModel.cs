using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WAVE.Dal.Entities;
using Action = WAVE.Dal.Entities.Action;

namespace WAVE.Website.Models
{
    public class EventsDetailsModel : LayoutModel
    {
        public Action Action { get; set; }
    }

    public class EventsIndexModel : LayoutModel
    {
        public EventsIndexModel()
        {
            Title = "Actions";
            Actions = new List<Action>();
            CategoryList = new List<CategoryItems>();
        }

        public List<CategoryItems> CategoryList { get; set; }
        public Category SelectedCategory { get; set; }
        public Filter Filter { get; set; }
        public Sort Sort { get; set; }
        public OrganizedBy OrganizedBy { get; set; }
        public Status Status { get; set; }
        public List<Action> Actions { get; set; }
    }

    public class EventsCreateModel : LayoutModel
    {
        [Required]
        [StringLength(60, MinimumLength = 10)]
        public new String Title { get; set; }

        [Required]
        public String Category { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Goal { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 160)]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public String Address { get; set; }

        [Required]
        public String Country { get; set; }

        [Required]
        public HttpPostedFileBase Image { get; set; }
    }

    public class EventsEditModel :EventsCreateModel 
    {
        public string ImageUrl { get; set; }
        public int Id { get; set; }
    }

    public class CategoryItems
    {
        public CategoryItems(Category category, bool selected)
        {
            Category = category;
            Selected = selected;
        }

        public Category Category { get; set; }
        public bool Selected { get; set; }
    }

    public enum Sort
    {
        Relevance,
        Title,
        Ending,
        Latest
    }

    public enum Filter
    {
        All,
        My
    }

    public enum OrganizedBy
    {
        Everybody,
        Organizations,
        Company,
        Users
    }

    public enum Status
    {
        All,
        Ongoing,
        Successful,
        Unsuccessful
    }
}