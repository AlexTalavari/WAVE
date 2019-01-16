using System;
using System.Diagnostics;
using WAVE.Dal.Entities;
using WAVE.Website.Models;
using Action = WAVE.Dal.Entities.Action;

namespace WAVE.Website.Classes
{
    public class Transformer
    {
        public static EventsEditModel GetModelFromEntity (Action action)
        {
            return  new EventsEditModel
            {
                Id = action.Id,
                Address = action.Adress,
                Title = action.Title,
                Country = action.Location,
                ImageUrl = action.ThumbnailPhoto.Url,
                StartDate = action.StartDate,
                EndDate = action.EndDate,
                StartTime = action.StartDate,
                EndTime = action.EndDate,
                Description = action.Description,
                Goal = action.MaxValueBar,
                Category = action.Category.Title,
                Latitude = action.Latitude,
                Longitude = action.Longtitude,

            };
        }

        public static UserEditModel GetModelFromEntity(User user)
        {
            return new UserEditModel
            {
                Name = user.Name,
                Surname = user.Surname,
                DateOfBirth = user.DateOfBirth ?? DateTime.Now,
                Phone = user.Phone,
                Website = user.Website,
                Facebook = user.Facebook,
                Twitter = user.Twitter,
                Linkedin = user.Linkedin,
                GooglePlus = user.GooglePlus,
                Youtube = user.Youtube,
                Description = user.Description,
                Adress = user.Adress,
                ZipCode = user.ZipCode,
                City = user.City,
                Country = user.Country
            };
        }
    }
}
