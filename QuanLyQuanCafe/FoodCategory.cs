using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanCafe.Models
{
    public class FoodCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}