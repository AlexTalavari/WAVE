using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Website.Helpers.DTOs;

namespace WAVE.Website.Controllers.Api
{
    public class HomePageController : ApiController
    {
        private readonly IRepository<Action> _actionRepository;

        public HomePageController(IRepository<Action> repo)
        {
            _actionRepository = repo;
        }


        // GET api/<controller>
        public IEnumerable<HomePageDto> Get()
        {
            var actions = _actionRepository.All().ToList(); //.Where(a=>a.IsSuggested).ToList();
            return Mapper.Map<List<Action>, List<HomePageDto>>(actions);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}