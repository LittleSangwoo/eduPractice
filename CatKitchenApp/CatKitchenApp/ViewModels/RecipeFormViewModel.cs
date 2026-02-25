using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CatKitchenApp.ViewModels
{
    public class RecipeFormViewModel
    {
        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Введите название рецепта")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        [Display(Name = "Название рецепта")]
        public string RecipeName { get; set; } = null!;

        [Required(ErrorMessage = "Добавьте описание")]
        [StringLength(150, ErrorMessage = "Описание не должно превышать 150 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Укажите время приготовления")]
        [Range(1, 1000, ErrorMessage = "Время должно быть больше 0")]
        [Display(Name = "Время (мин)")]
        public int CookingTime { get; set; }

        [Display(Name = "Путь к изображению (необязательно)")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        [Display(Name = "Категория")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Выберите автора")]
        [Display(Name = "Автор")]
        public int AuthorID { get; set; }

        // Списки для выпадающих меню (Drop-downs)
        public SelectList? Categories { get; set; }
        public SelectList? Authors { get; set; }
    }
}
