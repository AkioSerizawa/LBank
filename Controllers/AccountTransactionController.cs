using LBank.Data;
using LBank.Extensions;
using LBank.Models;
using LBank.Services;
using LBank.Utils;
using LBank.ViewModel;
using LBank.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBank.Controllers;

[ApiController]
public class AccountTransactionController : ControllerBase
{
    private AccountTransactionService accountTransactionService = new AccountTransactionService();

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

        var accountTransfer = await context
            .Accounts
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.AccountId == model.AccountTransferId);

        if (accountTransfer == null)
            return NotFound(
                new ResultViewModel<Account>(UtilMessages.account03XE02(accountTransfer.AccountId)));

        if (model.TransactionHistory == String.Empty)
        {
            try
            {
                var typeTransfer = await context
                    .TransactionTypes
                    .FirstOrDefaultAsync(x => x.TypeId == model.TypeId);

                if (typeTransfer == null)
                    return NotFound(new ResultViewModel<TransactionType>(UtilMessages.type05XE02(typeTransfer.TypeId)));

                model.TransactionHistory =
                    $"Tipo de movimentação {typeTransfer.TypeDescription} para {accountTransfer.User.UserName}";
            }
            catch (TimeoutException ex)
            {
                return StatusCode(408, new ResultViewModel<AccountTransaction>(UtilMessages.information01XE01(ex)));
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

        try
        {
            await context.AccountTransactions.AddAsync(transfer);
            await context.SaveChangesAsync();

            switch (transfer.TypeId)
            {
                case 1:
                    accountTransactionService.accountBalanceCreditDebit(
                        transfer.TransactionValue,
                        transfer.AccountId,
                        transfer.AccountTransferId);
                    break;
                case 2:
                    accountTransactionService.accountDeposit(model.TransactionValue, model.AccountTransferId);
                    break;
                case 3:
                    accountTransactionService.accountWithdraw(model.TransactionValue, model.AccountTransferId);
                    break;
            }

            return Ok(new ResultViewModel<dynamic>(new
            {
                transfer = accountTransfer.User.UserName, transfer.TransactionId, transfer.TransactionValue,
                transfer.TransactionHistory, transfer.TransactionDate, transfer.TransactionType.TypeDescription
            }));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE04(ex)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE01(ex)));
        }
    }

    [HttpGet("v1/account/transfer")]
    public async Task<IActionResult> GetAllAysnc(
        [FromServices] DataContext context)
    {
        try
        {
            var accountTransaction = await context
                .AccountTransactions
                .Where(x => x.AccountId == 1)
                .ToListAsync();

            if (accountTransaction.Count == 0)
                return NotFound(new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE05()));

            return Ok(new ResultViewModel<List<AccountTransaction>>(accountTransaction));
        }
        catch (TimeoutException ex)
        {
            return StatusCode(408, new ResultViewModel<AccountTransaction>(UtilMessages.information01XE01(ex)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE01(ex)));
        }
    }

    [HttpGet("v1/account/transfer/{id:int}")]
    public async Task<IActionResult> GetByTransferAsync(
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            var accountTransaction = await context
                .AccountTransactions
                .Where(x => x.AccountId == 1)
                .Where(x => x.AccountTransferId == id)
                .ToListAsync();

            if (accountTransaction.Count == 0)
                return NotFound(new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE06(id)));

            return Ok(new ResultViewModel<List<AccountTransaction>>(accountTransaction));
        }
        catch (TimeoutException ex)
        {
            return StatusCode(408, new ResultViewModel<AccountTransaction>(UtilMessages.information01XE01(ex)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE01(ex)));
        }
    }
}