using System.ComponentModel.DataAnnotations;

namespace CatKitchenApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
