using System.ComponentModel.DataAnnotations;

namespace LBank.ViewModel.Account;

public class TransferViewModel
{
    public string TransactionDocument { get; set; }
    public string TransactionHistory { get; set; }
    public DateTime TransactionDate { get; set; }

    [Required(ErrorMessage = "O valor da transação é obrigatorio")]
    public decimal TransactionValue { get; set; }

    [Required(ErrorMessage = "Tipo de transação é obrigatorio")]
    public int TypeId { get; set; }

    [Required(ErrorMessage = "Precisa ser informado para quem está transferindo")]
    public int AccountTransferId { get; set; }
}