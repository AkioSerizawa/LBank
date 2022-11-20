using System.ComponentModel.DataAnnotations;

namespace LBank.ViewModel.Account;

public class DepositViewModel
{
    public string TransactionDocument { get; set; }
    public string TransactionHistory { get; set; }

    [Required(ErrorMessage = "Data de deposito é obrigatoria")]
    public DateTime TransactionDate { get; set; }

    [Required(ErrorMessage = "O valor da transação é obrigatorio")]
    public decimal TransactionValue { get; set; }

    [Required(ErrorMessage = "Tipo de transação é obrigatorio")]
    public int TypeId { get; set; }

    [Required(ErrorMessage = "Precisa ser informado para quem está depositando")]
    public int AccountTransferId { get; set; }
}