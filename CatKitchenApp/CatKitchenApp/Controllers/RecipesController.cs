using CatKitchenApp.Data;
using CatKitchenApp.Models;
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
    public class RecipesController : Controller
    {
        private readonly CatKitchenDbContext _context;

        public RecipesController(CatKitchenDbContext context)
        {
            _context = context;
        }

        // ГЛАВНЫЙ МЕТОД (Один для всех требований: поиск, фильтр, сортировка)
        public async Task<IActionResult> Index(string searchString, int? categoryId, string sortOrder)
        {
// 1. Загружаем категории для выпадающего списка 
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");

            // 2. Базовый запрос с подгрузкой Автора и Категории
            var recipes = _context.Recipes.Include(r => r.Author).Include(r => r.Category).AsQueryable();
// 3. Поиск (Требование 3.a.iv) [cite: 3]
            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.RecipeName.Contains(searchString));
            }
// 4. Фильтрация (Требование 3.a.ii) 
            if (categoryId.HasValue)
            {
                recipes = recipes.Where(r => r.CategoryID == categoryId);
            }
// 5. Сортировка (Требование 3.a.iii) [cite: 2]
            switch (sortOrder)
            {
                case "Time": recipes = recipes.OrderBy(r => r.CookingTime); break;
                case "Name": recipes = recipes.OrderBy(r => r.RecipeName); break;
                default: recipes = recipes.OrderByDescending(r => r.RecipeID); break;
            }

            var model = await recipes.ToListAsync();
            // Заполняем счетчик для нижней панели 
            ViewBag.Count = model.Count;

            return View(model);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RecipeID == id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // GET: Recipes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorName");
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeID,RecipeName,Description,CookingTime,Image,CategoryID,AuthorID")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorName", recipe.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", recipe.CategoryID);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorName", recipe.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", recipe.CategoryID);
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeID,RecipeName,Description,CookingTime,Image,CategoryID,AuthorID")] Recipe recipe)
        {
            if (id != recipe.RecipeID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorName", recipe.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", recipe.CategoryID);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RecipeID == id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null) _context.Recipes.Remove(recipe);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeID == id);
        }

    }


}
