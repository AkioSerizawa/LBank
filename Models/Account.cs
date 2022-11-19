namespace LBank.Models;

public class Account
{
    public int AccountId { get; set; }
    public decimal AccountBalance { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }

    public IList<AccountTransaction> AccountTransaction { get; set; }
}