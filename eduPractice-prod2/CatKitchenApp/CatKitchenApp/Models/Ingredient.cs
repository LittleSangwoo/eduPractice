using System.ComponentModel.DataAnnotations;

namespace CatKitchenApp.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }

        [Required]
        [StringLength(50)]
        public string IngredientName { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
