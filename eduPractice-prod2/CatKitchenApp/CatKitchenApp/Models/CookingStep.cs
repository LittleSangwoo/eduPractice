using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class CookingStep
    {
        [Key]
        public int StepID { get; set; }

        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public Recipe Recipe { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string StepDescription { get; set; }
    }
}
