using LBank.Data;
using LBank.Interface;
using LBank.Models;

namespace LBank.Services;

public class AccountService : IAccountService
{
    public int AccountCreate(int userId)
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