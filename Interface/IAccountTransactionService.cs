namespace LBank.Interface;

public interface IAccountTransactionService
{
    public void accountBalanceCreditDebit(decimal valueTransfer, int accountId, int accountTransferId);
    public void accountDeposit(decimal valueDeposit, int accountId);
    public void accountWithdraw(decimal valueWithdraw, int accountId);
    public Task<List<string>> extractAccount(int accountId);
}