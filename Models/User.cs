namespace LBank.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserSlug { get; set; }
    public string UserPassword { get; set; }

    public IList<Account> Account { get; set; }
}