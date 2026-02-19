using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipeName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        public int CookingTime { get; set; }

        [StringLength(50)]
        public string Image { get; set; } // Путь к картинке

        // Внешние ключи
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public Author Author { get; set; }

        // Связи с другими таблицами
        public List<CookingStep> CookingSteps { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public List<Review> Reviews { get; set; }
        public List<RecipeTag> RecipeTags { get; set; }
    }
}
