using MeetingSchedule.Models;
namespace MeetingSchedule.Services
{
    public interface IUserService
    {
        User CreateUser(string name);
        User? GetUser(int id);
        List<User> GetAllUsers();
    }
}
