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
        private readonly IMeetingService meetingService;
        public UsersController(IUserService userService, IMeetingService meetingService)
        {
            this.userService = userService;
            this.meetingService = meetingService;
        }

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

        [HttpGet("{id}/meetings")]
        public ActionResult<List<Meeting>> GetUserMittings(int id)
        {
            if (userService.GetUser(id) is null)
                return NotFound();

            var meetings = meetingService.GetMeetingsForUser(id);
            return Ok(meetings);
        }

    }
}
