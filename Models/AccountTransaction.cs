namespace LBank.Models;

public class AccountTransaction
{
    public int TransactionId { get; set; }
    public string TransactionDocument { get; set; }
    public string TransactionHistory { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal TransactionValue { get; set; }
    public int TypeId { get; set; }
    public int AccountId { get; set; }
    public int AccountTransferId { get; set; }

    public Account Account { get; set; }
    public TransactionType TransactionType { get; set; }
}