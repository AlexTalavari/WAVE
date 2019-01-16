using AutoMapper;
using SAVE.Dal.Entities;
using SAVE.Dal.Repositories;
using SAVE.Website.Helpers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SAVE.Website.Controllers.Api
{
    [Authorize]
    public class UserDataController : ApiController
    {

        

        // GET api/<controller>/5
        public UserDataDto Get(String username)
        {
            if (HttpContext.Current.User.Identity.Name.ToLowerInvariant() != username.ToLowerInvariant())
            {
                throw new UnauthorizedAccessException();
            }
            var user = _userRepository.Get(u=>u.Account.UserName==username).First();
            user.UserNotifications.OrderByDescending(un => un.DateModified).Take(5).ToList();
            user.UserReceivedUserMessages.OrderByDescending(un => un.DateSent).Take(3).ToList();
            return Mapper.Map<User, UserDataDto>(user);

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}