using LBank.Models;

namespace LBank.Interface
{
    public interface ITransactionTypeService
    {
        public TransactionType GetTransactionTypeById(int typeId);
    }
}