namespace LBank.Utils;

public class UtilMessages
{
    #region User

    public static string user02XE01(Exception ex) => $"02XE01 - Falha interna no servidor - | {ex.Message} |";
    public static string user02XE02(Exception ex) => $"02XE01 - Não foi possível incluir o usuário - | {ex.Message} |";

    #endregion
}