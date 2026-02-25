using System.ComponentModel.DataAnnotations;

namespace CatKitchenApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее 50 символов")]
        public string AuthorName { get; set; } = null!;

        [Required(ErrorMessage = "Введите логин")]
        [StringLength(50, ErrorMessage = "Логин не может быть длиннее 50 символов")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        public string Mail { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
