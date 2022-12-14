using System.ComponentModel.DataAnnotations;

namespace LBank.ViewModel.User;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail invalido;")]
    public string UserEmail { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    public string UserPassword { get; set; }
}