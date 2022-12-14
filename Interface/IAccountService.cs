using LBank.Models;
using LBank.ViewModel.User;

namespace LBank.Interface;

public interface IAccountService
{
    public Task<Account> AccountCreateUser(int userId);
    public Task<Account> AccountCreate(AccountCreateViewModel model);
    public Account AccountUserById(int accountId);
    public Account AccountUserByUserId(int userId);
}