using LBank.Data;
using LBank.Interface;
using LBank.Models;
using Microsoft.EntityFrameworkCore;

namespace LBank.Services;
public class AccountService : IAccountService
{
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

    public int AccountCreateUser(int userId)
    {
        try
        {
            using var context = new DataContext();

            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            const decimal inicialAccountBalance = (decimal)100.00;
            var account = new Account
            {
                AccountBalance = inicialAccountBalance,
                UserId = user.UserId
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            return account.AccountId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}