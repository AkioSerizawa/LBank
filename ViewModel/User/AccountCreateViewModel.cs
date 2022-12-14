using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LBank.ViewModel.User
{
    public class AccountCreateViewModel
    {
        [Required(ErrorMessage = "Usuario é obrigatório")]
        public int UserId { get; set; }
        public decimal InicialAccountBalance { get; set; }
    }
}