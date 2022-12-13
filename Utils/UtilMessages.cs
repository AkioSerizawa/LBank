namespace LBank.Utils;

public class UtilMessages
{
    #region Information

    public static string information01XE01(Exception ex) =>
        $"01XE01 - TimeOut, Processamento tomou mais tempo do que o permitido | {ex.Message} |";

    #endregion

    #region User

    public static string user02XE01(Exception ex) => $"02XE01 - Falha interna no servidor - | {ex.Message} |";

    public static string user02XE02(Exception ex) =>
        $"02XE02 -  Este E-mail já está cadastrado ou falha ao incluir o usuario - | {ex.Message} |";
    
    public static string user02XE03() => $"02XE03 - Usuário ou senha inválida";
    public static string user02XE04() => $"02XE04 - Senha inválida";
    public static string user02XE05() => "02XE05 - Email já cadastrado";

    #endregion

    #region Account

    public static string account03XE01(Exception ex) => $"03XE01 - Falha interna no servidor - | {ex.Message} |";

    public static string account03XE02(int id) =>
        $"03XE02 - Nenhuma conta bancaria encontrada! | Conta pesquisada - '{id}' |";

    public static string account03XE03() => $"03XE03 - Nenhuma conta bancaria encontrada!";

    #endregion

    #region AccountTransaction

    public static string accountTransaction04XE01(Exception ex) =>
        $"04XE01 - Falha interna no servidor - | {ex.Message} |";

    public static string accountTransaction04XE02(decimal transactionValue) =>
        $"04XE02 - O Valor da transferencia não pode ser negativo - | Valor informado: $ {transactionValue} |";

    public static string accountTransaction04XE03(DateTime transactionDate) =>
        $"04XE03 - A data não pode ser menor que a data atual - | Data informada: {transactionDate} |";

    public static string accountTransaction04XE04(Exception ex) =>
        $"04XE04 - Falha ao fazer a movimentação - | {ex.Message} |";

    public static string accountTransaction04XE05() => $"04XE05 - Nenhuma movimentação bancaria encontrada!";

    public static string accountTransaction04XE06(int id) =>
        $"04XE06 - Nenhuma movimentação bancaria encontrada! | Conta pesquisada - '{id}' |";

    public static string accountTransaction04XE07(decimal valueTransfer) =>
        $"04XE07 - Valor da transferencia é maior que o saldo possuido | Valor da transferencia: $ {valueTransfer} |";

    #endregion

    #region TransactionType

    public static string type05XE01(Exception ex) =>
        $"05XE01 - Falha interna no servidor - | {ex.Message} |";

    public static string type05XE02(int id) =>
        $"05XE02 - Nenhuma tipo de movimentação encontrado! | Movimentação pesquisada - '{id}' |";

    #endregion
}