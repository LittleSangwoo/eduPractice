using CatKitchenApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CatKitchenApp.Data
{
    public class CatKitchenDbContext : DbContext
    {
        public CatKitchenDbContext(DbContextOptions<CatKitchenDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<CookingStep> CookingSteps { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RecipeImage> RecipeImages { get; set; }

    }
}
