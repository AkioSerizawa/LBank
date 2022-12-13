using LBank.Models;

namespace LBank.Interface
{
    public interface IUserService
    {
        public int CreateUser(User registerModel);
        public User GetUser(int userId);
        public User GetUserByEmail(string userEmail);
    }
}