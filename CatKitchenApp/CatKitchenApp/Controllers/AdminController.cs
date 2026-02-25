using CatKitchenApp.Data;
using CatKitchenApp.Models;
using CatKitchenApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CatKitchenApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly CatKitchenDbContext _context;

        public AdminController(CatKitchenDbContext context)
        {
            _context = context;
        }

        // --- 1. ПРОСМОТР, ПОИСК, ФИЛЬТРАЦИЯ И СОРТИРОВКА (как в 3a, но с кнопками управления) ---
        public async Task<IActionResult> Index(string searchTerm, int? categoryId, string sortOrder, int pageNumber = 1)
        {
            int pageSize = 10;
            var recipesQuery = _context.Recipes.Include(r => r.Category).Include(r => r.Author).AsQueryable();

            // Поиск
            if (!string.IsNullOrEmpty(searchTerm))
                recipesQuery = recipesQuery.Where(r => r.RecipeName.Contains(searchTerm) || r.Description.Contains(searchTerm));

            // Фильтрация
            if (categoryId.HasValue && categoryId.Value > 0)
                recipesQuery = recipesQuery.Where(r => r.CategoryID == categoryId.Value);

            // Сортировка
            var nameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var timeSortParam = sortOrder == "time" ? "time_desc" : "time";

            recipesQuery = sortOrder switch
            {
                "name_desc" => recipesQuery.OrderByDescending(r => r.RecipeName),
                "time" => recipesQuery.OrderBy(r => r.CookingTime),
                "time_desc" => recipesQuery.OrderByDescending(r => r.CookingTime),
                _ => recipesQuery.OrderBy(r => r.RecipeName), // По умолчанию ASC по имени
            };

            // Пагинация
            var count = await recipesQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages > 0 ? totalPages : 1));

            var items = await recipesQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var viewModel = new RecipeIndexViewModel
            {
                Recipes = items,
                PageNumber = pageNumber,
                TotalPages = totalPages,
                SearchTerm = searchTerm,
                SelectedCategoryId = categoryId,
                SortOrder = sortOrder,
                Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", categoryId)
            };

            return View(viewModel);
        }

        // --- 2. ДОБАВЛЕНИЕ (GET и POST) ---
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new RecipeFormViewModel
            {
                Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName"),
                Authors = new SelectList(await _context.Authors.ToListAsync(), "AuthorID", "AuthorName")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipe
                {
                    RecipeName = model.RecipeName,
                    Description = model.Description,
                    CookingTime = model.CookingTime,
                    Image = model.Image ?? "/images/placeholder.jpg",
                    CategoryID = model.CategoryID,
                    AuthorID = model.AuthorID
                };
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Перезагрузка списков при ошибке валидации
            model.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", model.CategoryID);
            model.Authors = new SelectList(await _context.Authors.ToListAsync(), "AuthorID", "AuthorName", model.AuthorID);
            return View(model);
        }

        // --- 3. РЕДАКТИРОВАНИЕ (GET и POST) ---
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            var model = new RecipeFormViewModel
            {
                RecipeID = recipe.RecipeID,
                RecipeName = recipe.RecipeName,
                Description = recipe.Description,
                CookingTime = recipe.CookingTime,
                Image = recipe.Image,
                CategoryID = recipe.CategoryID,
                AuthorID = recipe.AuthorID,
                Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", recipe.CategoryID),
                Authors = new SelectList(await _context.Authors.ToListAsync(), "AuthorID", "AuthorName", recipe.AuthorID)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecipeFormViewModel model)
        {
            if (id != model.RecipeID) return NotFound();

            if (ModelState.IsValid)
            {
                var recipe = await _context.Recipes.FindAsync(id);
                if (recipe == null) return NotFound();

                recipe.RecipeName = model.RecipeName; recipe.Description = model.Description;
                recipe.CookingTime = model.CookingTime; recipe.Image = model.Image ?? recipe.Image;
                recipe.CategoryID = model.CategoryID; recipe.AuthorID = model.AuthorID;

                _context.Update(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            model.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", model.CategoryID);
            model.Authors = new SelectList(await _context.Authors.ToListAsync(), "AuthorID", "AuthorName", model.AuthorID);
            return View(model);
        }

        // --- 4. УДАЛЕНИЕ (GET-подтверждение и POST-удаление) ---
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _context.Recipes.Include(r => r.Category).FirstOrDefaultAsync(r => r.RecipeID == id);
            if (recipe == null) return NotFound();
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
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
