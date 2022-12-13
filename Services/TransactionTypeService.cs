using LBank.Data;
using LBank.Interface;
using LBank.Models;

namespace LBank.Services;

public class TransactionTypeService : ITransactionTypeService
{
    public TransactionType GetTransactionTypeById(int typeId)
    {
        try
        {
            using var context = new DataContext();

            var transferType = context.TransactionTypes.FirstOrDefault(x => x.TypeId == typeId);

            return transferType;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}