using LBank.Data;
using LBank.Models;
using LBank.Services;
using LBank.Utils;
using LBank.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBank.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private AccountService accountService = new AccountService();

    [HttpGet("v1/account/")]
    public async Task<IActionResult> GetAccountAsync(
        [FromServices] DataContext context)
    {
        try
        {
            var account = await context
                .Accounts
                .ToListAsync();

            if (account.Count == 0)
                return NotFound(new ResultViewModel<Account>(UtilMessages.account03XE03()));

            return Ok(new ResultViewModel<List<Account>>(account));
        }
        catch (TimeoutException ex)
        {
            return StatusCode(408, new ResultViewModel<Account>(UtilMessages.information01XE01(ex)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Account>(UtilMessages.account03XE01(ex)));
        }
    }

    [HttpGet("v1/account/{id:int}")]
    public async Task<IActionResult> GetAccountByIdAsync(
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            var account = accountService.AccountUserById(id);
            if (account == null)
                return NotFound(new ResultViewModel<Account>(UtilMessages.account03XE02(id)));

            var accountView =
                $"| Num.Conta: {account.AccountId} | Saldo da Conta: {account.AccountBalance} | Usuario: {account.User.UserName} |";

            return Ok(new ResultViewModel<dynamic>(accountView, null));
        }
        catch (TimeoutException ex)
        {
            return StatusCode(408, new ResultViewModel<Account>(UtilMessages.information01XE01(ex)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Account>(UtilMessages.account03XE01(ex)));
        }
    }
}