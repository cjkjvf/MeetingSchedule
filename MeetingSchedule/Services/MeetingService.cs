using MeetingSchedule.DTOs;
using MeetingSchedule.Models;

namespace MeetingSchedule.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IUserService userService;
        private readonly List<Meeting> meetings = new();

        private int nextId = 1;

        public MeetingService(IUserService userService) => this.userService = userService;

        public Meeting CreateMeeting(MeetingRequestDTO req)
        {
            if (req.ParticipantIds is null || req.ParticipantIds.Count == 0)
                throw new ArgumentException("No participants");
            if (req.DurationMinutes <= 0)
                throw new ArgumentException("Duration must be > 0");

            foreach (var participant in req.ParticipantIds)
                if (userService.GetUser(participant) == null)
                    throw new ArgumentException($"User {participant} not found");


            var duration = TimeSpan.FromMinutes(req.DurationMinutes);
            var start = req.EarliestStart;
            var end = req.LatestEnd;

            var busySlot = meetings.Where(m => m.Participants.Any(id =>
            req.ParticipantIds.Contains(id))).OrderBy(m => m.StartTime).ToList();

            DateTime current = start;
            foreach (var meeting in busySlot)
            {
                var busyStart = meeting.StartTime;
                var busyEnd = meeting.EndTime;

                if (current + duration <= busyStart)
                    return AddMeeting(req, current, current + duration);

                if (busyEnd > current)
                    current = busyEnd;
            }


            if (current + duration <= end)
                return AddMeeting(req, current, current+duration);
            throw new InvalidOperationException("No available time slot");
        }

        private Meeting AddMeeting(MeetingRequestDTO req, DateTime start, DateTime end)
        {
            var meeting = new Meeting
            {
                Id = nextId++,
                Participants = req.ParticipantIds,
                StartTime = start,
                EndTime = end
            };
            meetings.Add(meeting);
            return meeting;
        }

        public List<Meeting> GetMeetingsForUser(int id) =>
            meetings.Where(m => m.Participants.Contains(id)).ToList(); 

        public List<Meeting> GetAllMeetings() => meetings;
    }
}
