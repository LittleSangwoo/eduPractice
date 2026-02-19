using System.ComponentModel.DataAnnotations;

namespace CatKitchenApp.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }

        public List<Author> Authors { get; set; }
    }
}
