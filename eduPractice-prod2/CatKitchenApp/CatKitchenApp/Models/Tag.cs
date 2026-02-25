using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }

        [Required]
        [StringLength(50)]
        public string TagName { get; set; }

        // Связь: Один тег может быть во многих рецептах (через таблицу RecipeTags)
        public List<RecipeTag> RecipeTags { get; set; }
    }
}
