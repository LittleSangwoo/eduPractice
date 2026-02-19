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
<<<<<<< HEAD
        public DbSet<Role> Roles { get; set; }
=======
>>>>>>> 05f4f90d23455601ec6338e9634a94c0ea9aac75
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
