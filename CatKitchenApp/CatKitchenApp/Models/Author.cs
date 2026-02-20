using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatKitchenApp.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required]
        [StringLength(50)]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required] 
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Stage { get; set; }

        [Required]
        [StringLength(50)]
        public string Mail { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        // Связь: Один автор -> Много рецептов
        public List<Recipe> Recipes { get; set; }
    }
}
