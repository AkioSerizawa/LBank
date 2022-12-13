using LBank.Data;
using LBank.Interface;
using LBank.Models;

namespace LBank.Services;
public class UserService : IUserService
{
    public int CreateUser(User registerModel)
    {
        try
        {
            using var context = new DataContext();

            context.Users.Add(registerModel);
            context.SaveChanges();

            return registerModel.UserId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public User GetUser(int userId)
    {
        try
        {
            using var context = new DataContext();

            var user = context.Users
            .FirstOrDefault(x => x.UserId == userId);

            return user;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public User GetUserByEmail(string userEmail)
    {
        try
        {
            using var context = new DataContext();
            
            var user = context.Users
            .FirstOrDefault(x => x.UserEmail.Contains(userEmail));

            return user;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}