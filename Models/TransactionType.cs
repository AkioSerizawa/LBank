namespace LBank.Models;

public class TransactionType
{
    public int TypeId { get; set; }
    public string TypeDescription { get; set; }

    public IList<AccountTransaction> AccountTransaction { get; set; }
}