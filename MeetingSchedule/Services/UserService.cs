using MeetingSchedule.Models;

namespace MeetingSchedule.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> users = new();
        private int nextUserID = 1;

        public User CreateUser(string name) 
        {
            var user = new User { Id = nextUserID++, Name = name };
            users.Add(user);
            return user;
        }

        public User? GetUser(int id)
        {
           return users.FirstOrDefault(x => x.Id == id);
        }
        public List<User> GetAllUsers() => users;
    }
}
