using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<BusinessLogic.Models.User>> Get()
        {
            return _usersService.GetUsers();
        }

        // GET api/values
        [HttpGet("{userId}")]
        public ActionResult<BusinessLogic.Models.User> Get(long userId)
        {
            return _usersService.GetUserFull(userId);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] BusinessLogic.Models.User user)
        {
            _usersService.AddUser(user);
        }

        // PUT api/values/5
        [HttpPut("{userId}")]
        public void Put(long userId, [FromBody] BusinessLogic.Models.User user)
        {
            _usersService.EditUser(user);
        }

        // DELETE api/values/5
        [HttpDelete("{userId")]
        public void Delete(long userId)
        {
            _usersService.DeleteUser(userId);
        }
    }
}
