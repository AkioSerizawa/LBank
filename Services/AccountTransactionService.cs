using LBank.Data;
using LBank.Interface;
using Microsoft.EntityFrameworkCore;

namespace LBank.Services;

public class AccountTransactionService : IAccountTransactionService
{
    public void accountBalanceCreditDebit(decimal valueTransfer, int accountId, int accountTransferId)
    {
        try
        {
            using var context = new DataContext();

            var account = context.Accounts.Include(x => x.User).FirstOrDefault(x => x.AccountId == accountId);
            var accountTrasfer = context.Accounts.Include(x => x.User)
                .FirstOrDefault(x => x.AccountId == accountTransferId);

            decimal valueBalanceDebit = balanceDebit(account.AccountBalance, valueTransfer);
            decimal valueBalanceCredit = balanceCredit(accountTrasfer.AccountBalance, valueTransfer);

            account.AccountBalance = valueBalanceDebit;
            accountTrasfer.AccountBalance = valueBalanceCredit;

            context.Update(account);
            context.Update(accountTrasfer);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private decimal balanceDebit(decimal accountBalance, decimal valueTransfer)
    {
        decimal balance = (accountBalance - valueTransfer);
        return balance;
    }

    private decimal balanceCredit(decimal accountBalance, decimal valueTransfer)
    {
        decimal balance = (accountBalance + valueTransfer);
        return balance;
    }

    public void accountDeposit(decimal valueDeposit, int accountId)
    {
        try
        {
            using var context = new DataContext();
            var account = context.Accounts.Include(x => x.User).FirstOrDefault(x => x.AccountId == accountId);

            var value = balanceDeposit(account.AccountBalance, valueDeposit);

            account.AccountBalance = value;

            context.Update(account);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private decimal balanceDeposit(decimal accountBalance, decimal valueDeposit)
    {
        decimal deposit = (accountBalance + valueDeposit);
        return deposit;
    }

    public void accountWithdraw(decimal valueWithdraw, int accountId)
    {
        try
        {
            using var context = new DataContext();
            var account = context.Accounts.Include(x => x.User).FirstOrDefault(x => x.AccountId == accountId);
            var value = balanceWithDraw(account.AccountBalance, valueWithdraw);

            account.AccountBalance = value;

            context.Update(account);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private decimal balanceWithDraw(decimal accountBalance, decimal valueWithdraw)
    {
        decimal deposit = (accountBalance - valueWithdraw);
        return deposit;
    }
}