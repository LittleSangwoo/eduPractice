using CatKitchenApp.Data;
using CatKitchenApp.Models;
using CatKitchenApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatKitchenApp.Controllers
{
    [Authorize] // Доступ только для авторизованных
    public class RecipesController : Controller
    {
        private readonly CatKitchenDbContext _context;

        public RecipesController(CatKitchenDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm, int? categoryId, string sortOrder, int pageNumber = 1)
        {
            int pageSize = 6; // Количество записей на страницу

            // Подгружаем связанные данные (Категории и Авторов)
            var recipesQuery = _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Author)
                .AsQueryable();

            // 1. Поиск по ключевым полям
            if (!string.IsNullOrEmpty(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.RecipeName.Contains(searchTerm) ||
                    r.Description.Contains(searchTerm));
            }

            // 2. Фильтрация по категории
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                recipesQuery = recipesQuery.Where(r => r.CategoryID == categoryId.Value);
            }

            // 3. Сортировка столбцов (ASC/DESC)
            var nameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var timeSortParam = sortOrder == "time" ? "time_desc" : "time";

            recipesQuery = sortOrder switch
            {
                "name_desc" => recipesQuery.OrderByDescending(r => r.RecipeName),
                "time" => recipesQuery.OrderBy(r => r.CookingTime),
                "time_desc" => recipesQuery.OrderByDescending(r => r.CookingTime),
                _ => recipesQuery.OrderBy(r => r.RecipeName), // По умолчанию ASC по имени
            };

            // 4. Пагинация
            var count = await recipesQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Защита от выхода за пределы страниц
            if (pageNumber > totalPages && totalPages > 0) pageNumber = totalPages;
            if (pageNumber < 1) pageNumber = 1;

            var items = await recipesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Формируем ViewModel
            var viewModel = new RecipeIndexViewModel
            {
                Recipes = items,
                PageNumber = pageNumber,
                TotalPages = totalPages,
                SearchTerm = searchTerm,
                SelectedCategoryId = categoryId,
                SortOrder = sortOrder,
                NameSort = nameSortParam,
                TimeSort = timeSortParam,
                Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", categoryId)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Author)
                .Include(r => r.CookingSteps.OrderBy(s => s.StepNumber)) // Подгружаем шаги
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient) // Подгружаем ингредиенты
                .Include(r => r.Reviews) // Подгружаем отзывы
                .FirstOrDefaultAsync(m => m.RecipeID == id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }
    }


}
