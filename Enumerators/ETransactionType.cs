using System.ComponentModel;

namespace LBank.Enumerators;

public enum ETransactionType
{
    [Description("Transação - Debito/Credito")]
    DebitoCredito = 1,

    [Description("Deposito")] Deposito = 2,

    [Description("Saque")] Saque = 3
}