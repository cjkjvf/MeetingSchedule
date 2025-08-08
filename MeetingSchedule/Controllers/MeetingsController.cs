using MeetingSchedule.DTOs;
using MeetingSchedule.Models;
using MeetingSchedule.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSchedule.Controllers
{
    [ApiController]
    [Route("meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService meetingService;
        public MeetingsController(IMeetingService meetingService) =>
            this.meetingService = meetingService;

        [HttpPost]
        public ActionResult<Meeting> Create([FromBody] MeetingRequestDTO req)
        {
            try
            {
                var meeting = meetingService.CreateMeeting(req);
                return Created("", meeting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public ActionResult<List<Meeting>> GetAll() =>
            Ok(meetingService.GetAllMeetings());
    }
}
