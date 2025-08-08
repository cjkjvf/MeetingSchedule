using MeetingSchedule.DTOs;
using MeetingSchedule.Models;

namespace MeetingSchedule.Services
{
    public interface IMeetingService
    {
        Meeting CreateMeeting(MeetingRequestDTO req);
        List<Meeting> GetMeetingsForUser(int id);
        List<Meeting> GetAllMeetings();
    }
}
