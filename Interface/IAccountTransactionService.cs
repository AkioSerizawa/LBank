using LBank.Models;

namespace LBank.Interface;

public interface IAccountTransactionService
{
    public Task CreateAccountTransactionAsync(AccountTransaction accountTransaction);
    public void AccountBalanceCreditDebit(decimal valueTransfer, int accountId, int accountTransferId);
    public void AccountDeposit(decimal valueDeposit, int accountId);
    public void AccountWithdraw(decimal valueWithdraw, int accountId);
    public Task<List<string>> ExtractAccount(int accountId);
}