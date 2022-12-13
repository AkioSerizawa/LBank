using LBank.Models;

namespace LBank.Interface;

public interface IAccountService
{
    public int AccountCreateUser(int userId);
    public Account AccountUserById(int accountId);
}