using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Website.Helpers.DTOs;

namespace WAVE.Website.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> repo)
        {
            _userRepository = repo;
        }

        // GET api/users
        [Authorize]
        public IEnumerable<UserDto> Get()
        {
            var users = _userRepository.All().ToList();
            return Mapper.Map<List<User>, List<UserDto>>(users);
            //users;
        }

        // GET api/users/5
        public IEnumerable<User> Get(string id)
        {
            var users = _userRepository.FilterBy(u => u.Surname == id).ToList();
            return users;
        }

        // POST api/users
        public void Post([FromBody] string value)
        {
        }

        // PUT api/users/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}