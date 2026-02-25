using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeIngredientsID { get; set; } // На схеме просто RecipeIngredients, но нужен ID

        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public Recipe Recipe { get; set; }

        public int IngredientID { get; set; }
        [ForeignKey("IngredientID")]
        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
}
