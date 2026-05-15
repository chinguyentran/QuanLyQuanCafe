using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanCafe.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int IdCategory { get; set; }

        public double Price { get; set; }

        [ForeignKey("IdCategory")]
        public virtual FoodCategory Category { get; set; }
    }
}