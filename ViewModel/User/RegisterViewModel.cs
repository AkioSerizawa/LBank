using System.ComponentModel.DataAnnotations;

namespace LBank.ViewModel.User;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Nome do usuario é obrigatorio")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "E-mail é obrigatorio")]
    [EmailAddress(ErrorMessage = "E-mail invalido")]
    public string UserEmail { get; set; }

    public string UserSlug { get; set; }

    [Required(ErrorMessage = "Senha é obrigatorio")]
    public string UserPassword { get; set; }
}