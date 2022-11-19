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

    #endregion

    #region Account

    public static string account03XE01(Exception ex) => $"03XE01 - Falha interna no servidor - | {ex.Message} |";

    public static string account03XE02(int id) =>
        $"03XE02 - Nenhuma conta bancaria encontrada! | Conta pesquisada - '{id}' |";

    public static string account03XE03() => $"03XE03 - Nenhuma conta bancaria encontrada!";

    #endregion
}