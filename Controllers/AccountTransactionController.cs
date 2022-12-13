using LBank.Data;
using LBank.Enumerators;
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
    private AccountService accountService = new AccountService();
    private TransactionTypeService transactionService = new TransactionTypeService();

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

        var account = accountService.AccountUserById(1);
        if (account == null)
            return NotFound(
                new ResultViewModel<Account>(UtilMessages.account03XE02(account.AccountId)));

        var accountTransfer = accountService.AccountUserById(model.AccountTransferId);
        if (accountTransfer == null)
            return NotFound(
                new ResultViewModel<Account>(UtilMessages.account03XE02(accountTransfer.AccountId)));

        if (model.TransactionValue > account.AccountBalance &&
            model.TypeId == (int)ETransactionType.DebitoCredito)
            return NotFound(
                new ResultViewModel<Account>(UtilMessages.accountTransaction04XE07(model.TransactionValue)));

        if (model.TransactionHistory == String.Empty)
        {
            try
            {
                var typeTransfer = transactionService.GetTransactionTypeById(model.TypeId);
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

        try
        {
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

            await accountTransactionService.CreateAccountTransactionAsync(transfer);

            switch (transfer.TypeId)
            {
                case (int)ETransactionType.DebitoCredito:
                    accountTransactionService.AccountBalanceCreditDebit(
                        transfer.TransactionValue,
                        transfer.AccountId,
                        transfer.AccountTransferId);
                    break;
                case (int)ETransactionType.Deposito:
                    accountTransactionService.AccountDeposit(model.TransactionValue, model.AccountTransferId);
                    break;
                case (int)ETransactionType.Saque:
                    accountTransactionService.AccountWithdraw(model.TransactionValue, model.AccountTransferId);
                    break;
            }

            string dateFormact = transfer.TransactionDate.ToString("dd/MM/yyyy");

            string extractMovimentation = $"Documento: {transfer.TransactionDocument} | Data da Movimentação: {dateFormact} | Valor: {transfer.TransactionValue} | De: {account.User.UserName} | Para: {accountTransfer.User.UserName} | Histórico: {transfer.TransactionHistory}";

            return Ok(new ResultViewModel<string>(@$"Movimentação realizada com sucesso - {extractMovimentation}", null));
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
    public async Task<IActionResult> GetAllAsync(
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

    [HttpGet("v1/account/extract/{id:int}")]
    public async Task<IActionResult> GetAccountExtract(
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            List<string> accountTransactionCollection = await accountTransactionService.ExtractAccount(id);

            if (accountTransactionCollection.Count == 0)
                return NotFound(new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE06(id)));

            return Ok(new ResultViewModel<dynamic>(accountTransactionCollection, null));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<AccountTransaction>(UtilMessages.accountTransaction04XE01(ex)));
        }
    }
}