using MeetingSchedule.DTOs;
using MeetingSchedule.Models;
using MeetingSchedule.Services;
using Microsoft.AspNetCore.Mvc;


namespace MeetingSchedule.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService) => this.userService = userService;

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] UserDTO dto)
        {
            var user = userService.CreateUser(dto.Name);
            return CreatedAtAction(nameof(GetUser), new {id = user.Id}, user);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id) =>
            userService.GetUser(id) is User user ? Ok(user) : BadRequest();

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers() => Ok(userService.GetAllUsers());

        //[HttpGet("{userId}/meetings")]

    }
}
