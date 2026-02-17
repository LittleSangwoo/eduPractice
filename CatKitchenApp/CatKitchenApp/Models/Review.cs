using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public Recipe Recipe { get; set; }

        [Required]
        [Column(TypeName = "text")] 
        public string ReviewText { get; set; }

        public int Rating { get; set; }
    }
}
