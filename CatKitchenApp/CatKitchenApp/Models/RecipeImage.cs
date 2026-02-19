using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class RecipeImage
    {
        [Key]
        public int ImageID { get; set; }

        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public Recipe Recipe { get; set; }

        [StringLength(50)]
        public string ImagePath { get; set; }
    }
}
