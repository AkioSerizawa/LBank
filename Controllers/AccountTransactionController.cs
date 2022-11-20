using System.Reflection.Metadata.Ecma335;
using LBank.Data;
using LBank.Extensions;
using LBank.Models;
using LBank.Utils;
using LBank.ViewModel;
using LBank.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBank.Controllers;

[ApiController]
public class AccountTransactionController : ControllerBase
{
    [HttpPost("v1/account/transfer")]
    public async Task<IActionResult> TransferAsync(
        [FromBody] TransferViewModel model,
        [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        if (model.TransactionValue < 0)
            return BadRequest(
                new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE02(model.TransactionValue)));

        if (model.TransactionDate < DateTime.Now.Date)
            return BadRequest(
                new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE03(model.TransactionDate)));

        if (model.TransactionHistory == String.Empty)
        {
            try
            {
                var typeTransfer = await context
                    .TransactionTypes
                    .FirstOrDefaultAsync(x => x.TypeId == model.TypeId);

                if (typeTransfer == null)
                    return NotFound(new ResultViewModel<TransactionType>(UtilMessages.type05XE02(typeTransfer.TypeId)));

                var accountTransfer = await context
                    .Accounts
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.AccountId == model.AccountTransferId);

                if (accountTransfer == null)
                    return NotFound(
                        new ResultViewModel<Account>(UtilMessages.account03XE02(accountTransfer.AccountId)));

                model.TransactionHistory =
                    $"Tipo de movimentação {typeTransfer.TypeDescription} para {accountTransfer.User.UserName}";
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE01(ex)));
            }
        }

        var transfer = new AccountTransaction
        {
            TransactionId = 0,
            TransactionDocument = model.TransactionDocument,
            TransactionHistory = model.TransactionHistory,
            TransactionDate = model.TransactionDate,
            TransactionValue = model.TransactionValue,
            TypeId = model.TypeId,
            AccountId = 1,
            AccountTransferId = model.AccountTransferId
        };

        return Ok();
    }
}