using CatKitchenApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatKitchenApp.ViewModels
{
    public class RecipeIndexViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();

        // Для фильтрации
        public SelectList? Categories { get; set; }
        public int? SelectedCategoryId { get; set; }

        // Для поиска
        public string? SearchTerm { get; set; }

        // Для сортировки
        public string? SortOrder { get; set; }
        public string? NameSort { get; set; }
        public string? TimeSort { get; set; }

        // Для пагинации
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
