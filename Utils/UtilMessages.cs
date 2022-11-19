namespace LBank.Utils;

public class UtilMessages
{
    #region User

    public static string user02XE01(Exception ex) => $"02XE01 - Falha interna no servidor - | {ex.Message} |";

    public static string user02XE02(Exception ex) =>
        $"02XE02 -  Este E-mail já está cadastrado ou falha ao incluir o usuario - | {ex.Message} |";

    public static string user02XE03() => $"02XE03 - Usuário ou senha inválida";
    public static string user02XE04() => $"02XE04 - Senha inválida";

    #endregion
}