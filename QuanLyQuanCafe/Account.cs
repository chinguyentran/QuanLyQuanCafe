using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanCafe.Models
{
    public class Account
    {
        [Key]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }
    }
}