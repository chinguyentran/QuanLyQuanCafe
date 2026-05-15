using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanCafe.Models
{
    public class TableFood
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
    }
}