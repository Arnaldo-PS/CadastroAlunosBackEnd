using System.ComponentModel.DataAnnotations;

namespace AlunosApi.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da Senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação da senha não são iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
