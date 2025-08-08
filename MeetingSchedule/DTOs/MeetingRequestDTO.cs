namespace MeetingSchedule.DTOs
{
    public class MeetingRequestDTO
    {
        public List<int> ParticipantIds { get; set; } = new List<int>();
        public int DurationMinutes { get; set; }
        public DateTime EarliestStart { get; set; }
        public DateTime LatestEnd { get; set; }
    }
}
