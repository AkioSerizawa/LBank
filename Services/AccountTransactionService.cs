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
            var accountTransfer = context.Accounts.Include(x => x.User)
                .FirstOrDefault(x => x.AccountId == accountTransferId);

            decimal valueBalanceDebit = balanceDebit(account.AccountBalance, valueTransfer);
            decimal valueBalanceCredit = balanceCredit(accountTransfer.AccountBalance, valueTransfer);

            account.AccountBalance = valueBalanceDebit;
            accountTransfer.AccountBalance = valueBalanceCredit;

            context.Update(account);
            context.Update(accountTransfer);
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

    public async Task<List<string>> extractAccount(int accountId)
    {
        try
        {
            using var context = new DataContext();

            List<string> extractAccountColections = new List<string>();

            var accountTransaction = await context
                .AccountTransactions
                .AsNoTracking()
                .Include(x => x.Account)
                .Include(x => x.TransactionType)
                .Include(x => x.Account.User)
                .Where(x => x.AccountId == accountId).OrderBy(x => x.TransactionDate).ToListAsync();

            // if (accountTransaction.Count == 0)
            //     return NotFound(new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE06(id)));

            foreach (var transactionItem in accountTransaction)
            {
                string dateFormact = transactionItem.TransactionDate.ToString("dd/MM/yyyy");

                string extract = $"Tipo de Movimentação: {transactionItem.TransactionType.TypeDescription}" +
                    $" | Histórico: {transactionItem.TransactionHistory} | Data: {dateFormact}" +
                    $" | Transferecia para: {transactionItem.Account.User.UserName} | Valor: {transactionItem.TransactionValue}";

                extractAccountColections.Add(extract);
            }

            return extractAccountColections;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}