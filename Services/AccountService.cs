using LBank.Data;
using LBank.Interface;
using LBank.Models;
using LBank.ViewModel.User;
using Microsoft.EntityFrameworkCore;

namespace LBank.Services;
public class AccountService : IAccountService
{
    private UserService userService = new UserService();

    public Account AccountUserById(int accountId)
    {
        try
        {
            using var context = new DataContext();

            var account = context
                .Accounts
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefault(x => x.AccountId == accountId);

            return account;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Account> AccountCreateUser(int userId)
    {
        try
        {
            using var context = new DataContext();

            var user = userService.GetUserById(userId);
            const decimal inicialAccountBalance = (decimal)100.00;
            var account = new Account
            {
                AccountBalance = inicialAccountBalance,
                UserId = user.UserId
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            return account;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Account> AccountCreate(AccountCreateViewModel model)
    {
        try
        {
            using var context = new DataContext();

            var account = new Account
            {
                AccountId = 0,
                AccountBalance = model.InicialAccountBalance,
                UserId = model.UserId,
            };

            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();

            return account;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Account AccountUserByUserId(int userId)
    {
        try
        {
            using var context = new DataContext();

            var account = context
                .Accounts
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefault(x => x.UserId == userId);

            return account;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}